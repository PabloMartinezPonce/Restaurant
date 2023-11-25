using Microsoft.EntityFrameworkCore;
using Restaurante.Data.DBModels;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class RolesDAO
    {

        #region CRUD Entidad Roles

        public async Task<ResponseModel> GetRoles()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var results = await db.Roles.AsNoTracking().OrderBy(x => x.Rol).ToListAsync();

                    if (results.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = results, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "No se encontraron roles." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        #endregion
    }
}