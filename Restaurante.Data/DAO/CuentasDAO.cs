using System.Data.SqlClient;
using System.Linq;
using System;
using Restaurant.Web;
using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Restaurant.Model;

namespace Restaurante.Data.DAO
{
    public class CuentasDAO
    {
        #region CRUD Entidad Cuenta

        MesasDAO daoMesa = new MesasDAO();
        ProductosDAO daoPro = new ProductosDAO();

        public async Task<ResponseModel> GetCuentaByIdEmpleado(int IdEmpleado)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var Cuentas = await db.Cuentas.Include("RelCuentaProductos").AsNoTracking().Where(cu => cu.CuentaActiva.Value == true && cu.IdEmpleado == IdEmpleado).ToListAsync();
                    var empleado = await db.Usuarios.AsNoTracking().Where(us => us.Id == IdEmpleado).FirstAsync();
                    foreach (var cuenta in Cuentas)
                    {
                        cuenta.NombreEmpleado = empleado.Nombre;
                        decimal total = 0;
                        foreach (var productos in cuenta.RelCuentaProductos)
                        {
                            var subtotal = Convert.ToDecimal(productos.Precio) * Convert.ToInt32(productos.Cantidad);
                            total = total + subtotal;

                            productos.IdProductoNavigation =
                                     (from usr in db.Productos.AsNoTracking()
                                      where usr.Id == productos.IdProducto
                                      select new Producto
                                      {
                                          Id = usr.Id,
                                          Nombre = usr.Nombre,
                                          Cantidad = usr.Cantidad,
                                          RutaImagen = usr.RutaImagen,
                                          PrecioVenta = usr.PrecioVenta,
                                          PrecioCosto = usr.PrecioCosto,
                                          Descripcion = usr.Descripcion,
                                      }).First();
                        }
                        cuenta.total = total;
                    }

                    if (Cuentas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = Cuentas, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Cuenta>(), message = "No se encontraron cuentas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Cuenta cuentas)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    cuentas.CuentaActiva = true;
                    cuentas.FechaApertura = GlobalConfig.GetMexDate();

                    db.Cuentas.Add(cuentas);
                    var resU = db.SaveChanges();

                    if (resU > 0)
                    {
                        await daoMesa.StatusOcupada(new Mesa { Id = cuentas.IdMesa, Ocupada = true });
                        return new ResponseModel { responseCode = 200, objectResponse = cuentas.Id, message = "Nueva cuenta abierta." };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se puedo abrir la cuenta." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> CreateAssistant(Cuenta cuentas)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    cuentas.CuentaActiva = true;
                    cuentas.FechaApertura = GlobalConfig.GetMexDate();

                    db.Cuentas.Add(cuentas);
                    var resU = db.SaveChanges();

                    if (resU > 0)
                    {
                        await daoMesa.StatusOcupada(new Mesa { Id = cuentas.IdMesa, Ocupada = true });
                        return new ResponseModel { responseCode = 200, objectResponse = cuentas.Id, message = "Nueva cuenta abierta." };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se puedo abrir la cuenta." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> AddProducto(RelCuentaProducto producto)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var result = await daoPro.GetById(producto.IdProducto.Value);
                    Producto productoBD = result.objectResponse;
                    producto.Precio = productoBD.PrecioVenta.ToString();
                    producto.Nombre = productoBD.Nombre;
                    producto.Descuento = productoBD.Descuento.ToString();

                    db.RelCuentaProductos.Add(producto);
                    var resU = db.SaveChanges();

                    if (resU > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = resU, message = "Producto Agregado." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se pudo agregar el producto a la cuenta." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> CreatePayment(Venta venta, int idMesa)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    venta.Fecha = GlobalConfig.GetMexDate();
                    db.Ventas.Add(venta);
                    var resU = db.SaveChanges();

                    if (resU > 0)
                    {
                        string error = string.Empty;
                        try
                        {
                            var result = await GetById(venta.IdCuenta);
                            Cuenta cuenta = result.objectResponse;
                            foreach (var producto in cuenta.RelCuentaProductos)
                                await daoPro.ControlStock(producto.Id);
                        }
                        catch (Exception ex) { error = error + " ControlStock: " + ex.Message; }
                        await StatusCuenta(venta.IdCuenta);

                        await daoMesa.StatusOcupada(new Mesa { Id = idMesa, Ocupada = false });
                        return new ResponseModel { responseCode = 200, objectResponse = resU, message = "Venta regitrada exitosamente." + "\n" + error };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se encontraron ventas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> StatusCuenta(int idCuenta)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Cuentas.Where(u => u.Id == idCuenta).First<Cuenta>();
                    regitro.CuentaActiva = false;

                    var result = await con.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "La venta ha sido registrada" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetAll()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var Cuentas = await db.Cuentas.Include("RelCuentaProductos").AsNoTracking().Where(cu => cu.CuentaActiva.Value == true).ToListAsync();

                    foreach (var cuenta in Cuentas)
                    {
                        decimal total = 0;
                        var empleado = await db.Usuarios.AsNoTracking().Where(us => us.Id == cuenta.IdEmpleado).FirstAsync();
                        cuenta.NombreEmpleado = empleado.Nombre;
                        foreach (var productos in cuenta.RelCuentaProductos)
                        {
                            var subtotal = Convert.ToDecimal(productos.Precio) * Convert.ToInt32(productos.Cantidad);
                            total = total + subtotal;

                            productos.IdProductoNavigation =
                                     (from usr in db.Productos.AsNoTracking()
                                      where usr.Id == productos.IdProducto
                                      select new Producto
                                      {
                                          Id = usr.Id,
                                          Nombre = usr.Nombre,
                                          Cantidad = usr.Cantidad,
                                          RutaImagen = usr.RutaImagen,
                                          PrecioVenta = usr.PrecioVenta,
                                          PrecioCosto = usr.PrecioCosto,
                                          Descripcion = usr.Descripcion,
                                      }).First();
                        }
                        cuenta.total = total;
                    }

                    if (Cuentas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = Cuentas, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Categoria>(), message = "No se encontraron cuentas." };
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
                Cuenta cuenta = new Cuenta();
                using (var db = new restauranteContext())
                {
                    cuenta = await db.Cuentas.Include("RelCuentaProductos").AsNoTracking().Where(usr => usr.Id == id).FirstAsync();
                    decimal total = 0;
                    foreach (var productos in cuenta.RelCuentaProductos)
                    {
                        var subtotal = Convert.ToDecimal(productos.Precio) * Convert.ToInt32(productos.Cantidad);
                        total = total + subtotal;

                        productos.IdProductoNavigation =
                                 (from usr in db.Productos.AsNoTracking()
                                  where usr.Id == productos.IdProducto
                                  select new Producto
                                  {
                                      Id = usr.Id,
                                      Nombre = usr.Nombre,
                                      Cantidad = usr.Cantidad,
                                      RutaImagen = usr.RutaImagen,
                                      PrecioVenta = usr.PrecioVenta,
                                      PrecioCosto = usr.PrecioCosto,
                                      Descripcion = usr.Descripcion,
                                  }).First();
                    }
                    cuenta.total = total;
                }

                if (cuenta.Id > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = cuenta, message = "Success" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = new Categoria(), message = "No se encontraron cuentas." };
            }
            catch (SqlException ex)
            {
                return new ResponseModel
                {
                    responseCode = 500,
                    objectResponse = new Categoria(),
                    message = ex.Message
                };
            }
        }

        public async Task<bool> Exist(int id)
        {
            try
            {
                Cuenta cuenta = new Cuenta();
                using (var db = new restauranteContext())
                {
                    cuenta = await db.Cuentas.AsNoTracking().Where(usr => usr.Id == id).FirstAsync();
                }

                if (cuenta.Id > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        #endregion
    }
}