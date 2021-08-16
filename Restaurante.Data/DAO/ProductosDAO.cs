using Microsoft.EntityFrameworkCore;
using Restaurant.Web;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class ProductosDAO
    {
        #region CRUD Entidad os

        public async Task<ResponseModel> Get()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.AsNoTracking().ToListAsync();

                    if (productos.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "No se encontraron usuarios." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Producto(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (productos.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = productos.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Producto>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetByName(string nombre)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.AsNoTracking().Where(e => e.Nombre == nombre).ToListAsync();

                    if (productos.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = productos.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Producto regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Productos.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Producto regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Productos.Where(u => u.Id == regitroView.Id).First<Producto>();
                    if (!string.IsNullOrEmpty(regitroView.Nombre)) regitro.Nombre = regitroView.Nombre;
                    if (!string.IsNullOrEmpty(regitroView.Codigo)) regitro.Nombre = regitroView.Nombre;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;
                    if (regitroView.PrecioCosto != 0) regitro.PrecioCosto = regitroView.PrecioCosto;
                    if (regitroView.PrecioVenta != 0) regitro.PrecioVenta = regitroView.PrecioVenta;
                    if (regitroView.Stock != 0) regitro.Stock = regitroView.Stock;
                    if (regitroView.Descuento != 0) regitro.Descuento = regitroView.Descuento;
                    if (regitroView.IdCategoria != 0) regitro.IdCategoria = regitroView.IdCategoria;
                    if (regitroView.IdProveedor != 0) regitro.IdProveedor = regitroView.IdProveedor;
                    regitro.Activo = regitroView.Activo;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser actualizado." };
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
                    var regitro = db.Productos.Where(u => u.Id == id).First<Producto>();
                    db.Productos.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El usuario fue eliminado." };
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