using Dapper;
using Restaurante.Model;
using Restaurante.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Data.Repositories.DynamicQuery
{
    public class DynamicRepository : IDynamicRepository
    {
        private string connectionString;
        public DynamicRepository(string _connectionString) => connectionString = _connectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(connectionString);
        }

        public async Task<ResponseModel> DynamicInsert(dynamic model, string query)
        {
            try
            {
                using (var db = dbConnection())
                {
                    int result = 0;
                    var usuario = new UsuarioDTO();

                    if (model.GetType().Name == usuario.GetType().Name)
                        result = await db.ExecuteAsync(query, (UsuarioDTO)model);

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = true, message = "El registro se guardó exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = false, message = "El registro no pudo ser guardado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = null, message = ex.Message };
            }
        }

        public async Task<ResponseModel> DynamicDelete(int id, string query)
        {
            try
            {
                using (var db = dbConnection())
                {
                    var result = await db.ExecuteAsync(query, id);

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = true, message = "El registro se eliminó exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = false, message = "El registro no pudo ser eliminado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = null, message = ex.Message };
            }
        }

        public async Task<ResponseModel> DynamicSelect(int id, string queryAll, string queryById)
        {
            try
            {
                using (var db = dbConnection())
                {
                    dynamic result;
                    if (id == 0 && queryAll.Contains("Usuarios"))
                        result = await db.QueryAsync<UsuarioDTO>(queryAll);
                    else if (id == 0 && queryAll.Contains("Roles"))
                        result = await db.QueryAsync<RolDTO>(queryAll);
                    else
                        result = await db.ExecuteAsync(queryById, id);

                    if (result != null)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Consulta exitosa." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = result, message = "No se encontraron registros." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = null, message = ex.Message };
            }
        }
    }
}
