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
    public class MedicionesDAO
    {
        #region CRUD Entidad Mediciones

        public async Task<ResponseModel> GetAll(bool estaActivo)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var cajaChica = await db.Medicionescantidads.AsNoTracking().Where(asd => asd.Activo == estaActivo).ToListAsync();

                    if (cajaChica.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = cajaChica, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Cajachica>(), message = "No se encontraron mediciones." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Cajachica>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var cajaChica = await db.Medicionescantidads.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (cajaChica.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = cajaChica.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Cajachica(), message = "No se encontraron mediciones." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Cajachica(), message = ex.Message };
            }
        }

        #endregion
    }
}