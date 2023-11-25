using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurante.Data.DAO
{
    public class MesasDAO
    {
        #region CRUD Entidad Mesas

        public async Task<ResponseModel> Get()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var mesas = await db.Mesas.AsNoTracking().Where(cr => cr.Ocupada == false).ToListAsync();

                    if (mesas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = mesas, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Mesa>(), message = "No se encontraron mesas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Mesa>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var mesa = await db.Mesas.FindAsync(id);

                    if (mesa != null)
                        return new ResponseModel { responseCode = 200, objectResponse = mesa, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Mesa(), message = "No se encontraron mesas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Mesa regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Mesas.Add(regitro);
                    var result = await db.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa guardada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Mesa regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Mesas.Where(u => u.Id == regitroView.Id).First<Mesa>();
                    //if (!string.IsNullOrEmpty(regitroView.)) regitro.Mesa1 = regitroView.Mesa1;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;

                    var result = await con.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa guardada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
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
                    var regitro = db.Mesas.Where(u => u.Id == id).First<Mesa>();
                    db.Mesas.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa eliminada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser eliminada." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> StatusOcupada(Mesa regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Mesas.Where(u => u.Id == regitroView.Id).First<Mesa>();
                    regitro.Ocupada = regitroView.Ocupada;

                    var result = await con.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
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