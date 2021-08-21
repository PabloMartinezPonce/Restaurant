using Microsoft.EntityFrameworkCore;
using Restaurant.Web;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class ComplementosDAO
    {
        #region CRUD Entidad Complementos

        public async Task<ResponseModel> GetAllComplementos()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Complementos.Include("IdTipoComplementoNavigation").AsNoTracking().ToListAsync();

                    if (categorias.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Complementos.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (categorias.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetAllTipos()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var result = await db.Complementos.AsNoTracking().Include("IdTipoComplementoNavigation").ToListAsync();
                    
                    if (result.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Complemento regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Complementos.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El usuario no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Complemento regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Complementos.Where(u => u.Id == regitroView.Id).First<Complemento>();
                    if (!string.IsNullOrEmpty(regitroView.Nombre)) regitro.Nombre = regitroView.Nombre;
                    if (regitroView.Precio != 0) regitro.Precio = regitroView.Precio;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El complemento no existe." };
                }
            }
            catch (SqlException ex)
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
                    var regitro = db.Complementos.Where(u => u.Id == id).First<Complemento>();
                    db.Complementos.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El complemento no pudo ser eliminada." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        #endregion
    }
}