using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;

namespace Restaurante.Data.DAO
{
    public class LoginDAO
    {
        public async Task<ResponseModel> ValidateLogin(string usuario, string contrasena)
        {
            using (var db = new restauranteContext())
            {
                List<Usuario> result = await db.Usuarios.Where(usr => usr.NombreUsuario == usuario).ToListAsync();

                if (result.Count > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result.First(), message = "Success" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
            }
        }
    }
}
