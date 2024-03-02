using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using Restaurant.Repository.Interfaces;
//using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;

namespace Restaurante.Data.DAO
{
    public class UsuarioDAO : IUsuariosDAO
    {
        #region CRUD Entidad Usuario
        private static ObjectCache cache = MemoryCache.Default;

        //public Usuario GetUserLogin(LoginRequestModel login)
        //{
        //    try
        //    {
        //        using (var db = new restauranteContext())
        //        {
        //            ICollection<Usuarios> usuarios;

        //            if (login.esCorreo)
        //                usuarios = db.Usuarios.AsNoTracking().Where(e => e.correoElectronico == login.userName).ToList();
        //            else
        //                usuarios = db.Usuarios.AsNoTracking().Where(e => e.nombreUsuario == login.userName).ToList();

        //            if (usuarios.Count() >= 1)
        //                return usuarios).First();
        //            else
        //                return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<ResponseModel> GetUsers()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    List<Usuario> usuarios = await db.Usuarios.Include("IdRolNavigation").AsNoTracking().ToListAsync();

                    if (usuarios.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = usuarios, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "No se encontraron usuarios." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetUserById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usuarios = await db.Usuarios.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (usuarios.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = usuarios.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetUserByUserName(string userName)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usuarios = await db.Usuarios.AsNoTracking().Where(e => e.NombreUsuario == userName).ToListAsync();

                    if (usuarios.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = usuarios.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetUserByRol(int idRol)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usuarios = await db.Usuarios.AsNoTracking().Where(e => e.IdRol == idRol).ToListAsync();

                    if (usuarios.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = usuarios, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Usuario>(), message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> GetUserByRolCacje(int idRol)
        {
            try
            {
                string claveCache = "miClaveDeDatos";
                var datos = cache[claveCache] as ResponseModel;

                if (datos == null)
                {
                    // La data no está en la caché, así que la obtenemos de la base de datos
                    using (var db = new restauranteContext())
                    {
                        var usuarios = await db.Usuarios.AsNoTracking().Where(e => e.IdRol == idRol).ToListAsync();

                        if (usuarios.Count > 0)
                            datos = new ResponseModel { responseCode = 200, objectResponse = usuarios, message = "Success" };
                        else
                            datos = new ResponseModel { responseCode = 404, objectResponse = new List<Usuario>(), message = "El usuario no existe." };
                    }

                    // Almacenar en la caché
                    CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) };
                    cache.Add(claveCache, datos, policy);
                }

                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseModel> CreateUser(Usuario usuario)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Usuarios.Add(usuario);
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

        public async Task<ResponseModel> UpdateUser(Usuario usuario)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var usr = con.Usuarios.Where(u => u.Id == usuario.Id).First<Usuario>();
                    if (!string.IsNullOrEmpty(usuario.Nombre)) usr.Nombre = usuario.Nombre;
                    if (!string.IsNullOrEmpty(usuario.Apellido)) usr.Apellido = usuario.Apellido;
                    if (!string.IsNullOrEmpty(usuario.NombreUsuario)) usr.NombreUsuario = usuario.NombreUsuario;
                    if (!string.IsNullOrEmpty(usuario.Contrasena)) usr.Contrasena = usuario.Contrasena;
                    if (!string.IsNullOrEmpty(usuario.Descripcion)) usr.Descripcion = usuario.Descripcion;
                    if (!string.IsNullOrEmpty(usuario.Direccion)) usr.Direccion = usuario.Direccion;
                    if (!string.IsNullOrEmpty(usuario.RutaFoto)) usr.RutaFoto = usuario.RutaFoto;
                    if (!string.IsNullOrEmpty(usuario.CorreoElectronico)) usr.CorreoElectronico = usuario.CorreoElectronico;
                    if (usuario.Estatus != null) usr.Estatus = usuario.Estatus;
                    if (usuario.IdRol != null) usr.IdRol = usuario.IdRol;

                    var result = await con.SaveChangesAsync();
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

        public async Task<ResponseModel> ModifyStatus(string userName, bool estatus)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usr = db.Usuarios.Where(u => u.NombreUsuario == userName).First<Usuario>();
                    usr.Estatus = estatus;

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> UpdateImage(int id, string fileName)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usr = db.Productos.Where(u => u.Id == id).First<Producto>();
                    usr.RutaImagen = fileName.Trim();

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = usr, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> DeleteUser(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var usr = db.Usuarios.Where(u => u.Id == id).First<Usuario>();
                    db.Usuarios.Remove(usr);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El usuario no pudo ser eliminada." };
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
