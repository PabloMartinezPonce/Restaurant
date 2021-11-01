using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using System;

namespace Restaurante.Data.DAO
{
    public class ProductosDAO
    {
        #region CRUD Entidad Productos

        public async Task<ResponseModel> GetAll()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.Include("RelProductoReceta").Include("IdCategoriaNavigation").AsNoTracking().ToListAsync();

                    if (productos.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Producto>(), message = "No se encontraron usuarios." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Producto>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Get()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.AsNoTracking().Where(pro => pro.Activo == true).ToListAsync();

                    if (productos.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Producto>(), message = "No se encontraron usuarios." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Producto>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetAsComplement()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.Where(x => x.IdCategoria == 5)
                    .Select(pro => new Producto
                    {
                        Id = pro.Id,
                        Nombre = pro.Nombre,
                    }).ToListAsync();

                    if (productos.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Producto(), message = "No se encontraron usuarios." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Producto(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetByFilter(int id, string filter)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    List<Producto> productos = new List<Producto>();
                    if (filter == "Categoria")
                        productos = await db.Productos.AsNoTracking().Where(pr => pr.IdCategoria == id).ToListAsync();
                    else
                        productos = await db.Productos.AsNoTracking().ToListAsync();

                    if (productos.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Producto(), message = "No se encontraron usuarios." };
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
                    var producto = await db.Productos.Include("RelProductoComplementos").AsNoTracking().Where(e => e.Id == id).FirstOrDefaultAsync<Producto>();
                    var listaReceta = await db.RelProductoReceta
                        .Include("IdIngredienteNavigation").Include("IdProductoNavigation")
                        .Where(e => e.IdProducto == id)
                        .AsNoTracking().ToListAsync();

                    if (producto != null)
                    {
                        producto.RelProductoReceta = listaReceta;
                        return new ResponseModel { responseCode = 200, objectResponse = producto, message = "Success" };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Producto(), message = "El producto no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Producto(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetByName(string nombre)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var productos = await db.Productos.AsNoTracking().Where(e => e.Nombre == nombre).FirstOrDefaultAsync<Producto>();

                    if (productos != null)
                        return new ResponseModel { responseCode = 200, objectResponse = productos, message = "Success" };
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
                    regitro.Complementos = string.Empty;
                    regitro.ComplementosSelect = string.Empty;
                    regitro.Activo = true;
                    db.Productos.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue registrado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser creado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> AddIngredient(RelProductoRecetum regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    //regitro.Activo = true;
                    db.RelProductoReceta.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = regitro.Id, message = "El ingrediente fue registrado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El ingrediente no pudo ser creado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> DeleteIngredient(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var regitro = db.RelProductoReceta.Where(u => u.Id == id).First<RelProductoRecetum>();
                    db.RelProductoReceta.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El ingrediente fue eliminado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El ingrediente no pudo ser eliminado." };
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
                    if (!string.IsNullOrEmpty(regitroView.Codigo)) regitro.Codigo = regitroView.Codigo;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;
                    if (!string.IsNullOrEmpty(regitroView.ComplementosSelect)) regitro.ComplementosSelect = regitroView.ComplementosSelect;
                    if (!string.IsNullOrEmpty(regitroView.TipoMedicion)) regitro.TipoMedicion = regitroView.TipoMedicion;
                    if (regitroView.PrecioCosto != 0) regitro.PrecioCosto = regitroView.PrecioCosto;
                    if (regitroView.PrecioVenta != 0) regitro.PrecioVenta = regitroView.PrecioVenta;
                    if (regitroView.Stock != 0) regitro.Stock = regitroView.Stock;
                    if (regitroView.Descuento != 0) regitro.Descuento = regitroView.Descuento;
                    if (regitroView.IdCategoria != 0) regitro.IdCategoria = regitroView.IdCategoria;
                    if (regitroView.IdProveedor != 0) regitro.IdProveedor = regitroView.IdProveedor;
                    if (regitroView.IdTipoComplemento != 0) regitro.IdTipoComplemento = regitroView.IdTipoComplemento;
                    if (!string.IsNullOrEmpty(regitroView.TipoMedicion)) regitro.TipoMedicion = regitroView.TipoMedicion;
                    regitro.EsMultiStock = regitroView.EsMultiStock;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser actualizado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> ControlStock(int idProducto, int unidadesVendidas)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    int result = 0;
                    var regitro = con.Productos.Where(u => u.Id == idProducto).First<Producto>();
                    if (regitro.EsMultiStock)
                    {
                        var regitroRel = await con.RelProductoReceta.Where(u => u.IdProducto == idProducto).ToListAsync();
                        foreach (var item in regitroRel)
                        {
                            var restar = 0.0;
                            var ingrediente = await con.Ingredientes.Where(u => u.Id == item.IdIngrediente).FirstOrDefaultAsync();
                            var totalRestar = unidadesVendidas * item.Cantidad;
                            if ((ingrediente.TipoMedicion == "mililitro(s)" && ingrediente.TipoMedicionStock == "litro(s)") ||
                                (ingrediente.TipoMedicion == "gramo(s)" && ingrediente.TipoMedicionStock == "kilo(s)"))
                                restar = totalRestar * 0.00100;
                            else
                                restar = totalRestar;

                            ingrediente.Stock = ingrediente.Stock - restar;
                            result = await con.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        var totalRestar = unidadesVendidas * regitro.Cantidad;
                        regitro.Stock = regitro.Stock - totalRestar;
                        result = await con.SaveChangesAsync();
                    }

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser actualizado." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> SetActivity(bool estatus, int Id)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Productos.Where(u => u.Id == Id).First<Producto>();
                    regitro.Activo = !estatus;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue actualizado exitosamente." };
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
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue eliminado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser eliminado." };
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