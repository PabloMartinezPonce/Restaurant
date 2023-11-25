using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Restaurante.Data.DAO
{
    public class IngredientesDAO
    {
        #region CRUD Entidad Ingredientes

        public async Task<ResponseModel> GetAll(bool? buscarActivos)
        {
            try
            {
                var cajaChica = new List<Ingrediente>();
                using (var db = new restauranteContext())
                {
                    if (buscarActivos == null)
                        cajaChica = await db.Ingredientes.AsNoTracking().ToListAsync();
                    else
                        cajaChica = await db.Ingredientes.AsNoTracking().Where(asd => asd.Activo == buscarActivos).ToListAsync();

                    if (cajaChica.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = cajaChica, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Ingrediente>(), message = "No se encontraron ingredientes." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Ingrediente>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var cajaChica = await db.Ingredientes.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (cajaChica.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = cajaChica.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Ingrediente(), message = "No se encontraron ingredientes." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Ingrediente(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Ingrediente regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Ingredientes.Where(u => u.Id == regitroView.Id).First<Ingrediente>();
                    if (!string.IsNullOrEmpty(regitroView.Nombre)) regitro.Nombre = regitroView.Nombre;
                    if (!string.IsNullOrEmpty(regitroView.TipoMedicion)) regitro.TipoMedicion = regitroView.TipoMedicion;
                    if (!string.IsNullOrEmpty(regitroView.TipoMedicionStock)) regitro.TipoMedicionStock = regitroView.TipoMedicionStock;
                    if (regitroView.Stock != 0) regitro.Stock = regitroView.Stock;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El ingredientes fue actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El ingredientes no pudo ser actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Ingrediente regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    regitro.Activo = true;
                    db.Ingredientes.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Ingrediente registrado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El ingrediente no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var regitro = db.Ingredientes.Where(u => u.Id == id).First<Ingrediente>();
                    db.Ingredientes.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Ingrediente eliminado exitosamente" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El ingrediente no pudo ser eliminado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        #endregion
    }
}