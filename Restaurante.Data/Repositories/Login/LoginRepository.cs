using Dapper;
using Restaurante.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.Data.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private string connectionString;
        public LoginRepository(string _connectionString) => connectionString = _connectionString;

        protected SqlConnection dbConnection()
        {
            return new SqlConnection(connectionString);
        }

        public Task<ResponseModel> DisableAccount(string usuario, string query)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> ValidateLogin(string usuario, string contrasena, string query)
        {
            try
            {
                using (var db = dbConnection())
                {
                    var usuarioDTO = new UsuarioDTO { nombreUsuario = usuario };
                    var result = await db.QueryAsync<UsuarioDTO>(query, usuarioDTO);

                    List<UsuarioDTO> asList = result.AsList();
                    if (asList.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = asList[0], message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = null, message = ex.Message };
            }
        }
    }
}
