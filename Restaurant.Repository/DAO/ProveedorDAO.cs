using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class ProveedoresDAO
    {
        #region CRUD Entidad Proveedores

        public async Task<ResponseModel> GetAllProveedores()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var Proveedores = await db.Proveedores.AsNoTracking().ToListAsync();

                    if (Proveedores.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = Proveedores, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Proveedore>(), message = "No se encontraron proveedores." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Proveedore>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var Proveedores = await db.Proveedores.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (Proveedores.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = Proveedores.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Proveedore(), message = "No se encontraron proveedores." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Proveedore(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Proveedore regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Proveedores.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Proveedore regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Proveedores.Where(u => u.Id == regitroView.Id).First<Proveedore>();
                    if (!string.IsNullOrEmpty(regitroView.NombreContacto)) regitro.NombreContacto = regitroView.NombreContacto;
                    if (!string.IsNullOrEmpty(regitroView.Telefono)) regitro.Telefono = regitroView.Telefono;
                    if (!string.IsNullOrEmpty(regitroView.Direccion)) regitro.Direccion = regitroView.Direccion;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no existe." };
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
                    var regitro = db.Proveedores.Where(u => u.Id == id).First<Proveedore>();
                    db.Proveedores.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no pudo ser eliminada." };
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