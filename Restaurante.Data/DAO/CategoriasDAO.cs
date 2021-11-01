using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class CategoriasDAO
    {
        #region CRUD Entidad Categorias

        public async Task<ResponseModel> GetAllCategorias()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Categorias.AsNoTracking().ToListAsync();

                    if (categorias.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Categoria>(), message = "No se encontraron categorías." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Categoria>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Categorias.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (categorias.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Categoria(), message = "No se encontraron categorías." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Categoria(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Categoria regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Categorias.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Categoría guardada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El usuario no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Categoria regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Categorias.Where(u => u.Id == regitroView.Id).First<Categoria>();
                    if (!string.IsNullOrEmpty(regitroView.Nombre)) regitro.Nombre = regitroView.Nombre;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;
                    if (!string.IsNullOrEmpty(regitroView.RutaImagen)) regitro.RutaImagen = regitroView.RutaImagen;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Categoría guardada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no existe." };
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
                    var regitro = db.Categorias.Where(u => u.Id == id).First<Categoria>();
                    db.Categorias.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Categoría eliminada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no pudo ser eliminada." };
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