using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using Restaurant.Model;
using Restaurant.Repository.Interfaces;

namespace Restaurante.Data.DAO
{
    public class CuentasDAO : ICuentasDAO
    {
        #region CRUD Entidad Cuenta

        IMesasDAO _daoMesa;
        IProductosDAO _daoPro;

        public CuentasDAO(IProductosDAO daoPro, IMesasDAO daoMesa)
        {
            _daoPro = daoPro;
            _daoMesa = daoMesa;
        }

        public async Task<ResponseModel> GetCuentaByIdEmpleado(int IdEmpleado, bool estaActiva)
        {
            var cuentas = new List<Cuenta>();
            using (var db = new restauranteContext())
            {
                // Incluye los detalles necesarios en una sola consulta
                cuentas = await db.Cuentas
                               .Include(c => c.RelCuentaProductos)
                                   .ThenInclude(r => r.IdProductoNavigation)
                               .Where(c => c.CuentaActiva.Value == estaActiva &&
                                           c.IdEmpleado == IdEmpleado &&
                                           (c.IdCorte == null || c.IdCorte == 0))
                               .ToListAsync();
            }

            if (!cuentas.Any())
                return new ResponseModel { responseCode = 404, objectResponse = new List<Cuenta>(), message = "No se encontraron cuentas." };

            // Calcular el total y asignar el nombre del empleado
            Parallel.ForEach(cuentas, cuenta =>
            {
                cuenta.NombreEmpleado = cuenta.IdEmpleado.ToString();
                cuenta.total = cuenta.RelCuentaProductos.Sum(p => Convert.ToDecimal(p.Precio) * Convert.ToDecimal(p.Cantidad));
            });

            return new ResponseModel { responseCode = 200, objectResponse = cuentas, message = "Success" };
        }

        public async Task<ResponseModel> Create(Cuenta cuentas)
        {
            using (var db = new restauranteContext())
            {
                cuentas.CuentaActiva = true;
                cuentas.FechaApertura = GlobalConfig.GetMexDate();

                db.Cuentas.Add(cuentas);
                var resU = db.SaveChanges();

                if (resU > 0)
                {
                    await _daoMesa.StatusOcupada(new Mesa { Id = cuentas.IdMesa, Ocupada = true });
                    return new ResponseModel { responseCode = 200, objectResponse = cuentas.Id, message = "Nueva cuenta abierta." };
                }
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se puedo abrir la cuenta." };
            }
        }

        public async Task<ResponseModel> Cancel(int idCuenta)
        {
            using (var db = new restauranteContext())
            {
                var cuenta = db.Cuentas.Where(u => u.Id == idCuenta).First<Cuenta>();
                cuenta.CuentaActiva = false;
                var resU = db.SaveChanges();

                if (resU > 0)
                {
                    await _daoMesa.StatusOcupada(new Mesa { Id = cuenta.IdMesa, Ocupada = false });
                    return new ResponseModel { responseCode = 200, objectResponse = cuenta.Id, message = "La cuenta fue cancelada." };
                }
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se puedo cancelar la cuenta." };
            }
        }

        public async Task<ResponseModel> CreateAssistant(Cuenta cuentas)
        {
            using (var db = new restauranteContext())
            {
                cuentas.CuentaActiva = true;
                cuentas.FechaApertura = GlobalConfig.GetMexDate();

                db.Cuentas.Add(cuentas);
                var resU = db.SaveChanges();

                if (resU > 0)
                {
                    await _daoMesa.StatusOcupada(new Mesa { Id = cuentas.IdMesa, Ocupada = true });
                    return new ResponseModel { responseCode = 200, objectResponse = cuentas.Id, message = "Nueva cuenta abierta." };
                }
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se puedo abrir la cuenta." };
            }
        }

        public async Task<ResponseModel> AddProducto(RelCuentaProducto producto)
        {
            using (var db = new restauranteContext())
            {
                var result = await _daoPro.GetById(producto.IdProducto.Value);
                if (result == null || result.objectResponse == null)
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se pudo agregar el producto a la cuenta." };

                Producto productoBD = result.objectResponse;
                if (!string.IsNullOrEmpty(producto.NombreComplemento))
                    producto.Precio = (productoBD.PrecioVenta + Convert.ToDecimal(producto.PrecioComplemento)).ToString();
                else
                    producto.Precio = productoBD.PrecioVenta.ToString();

                producto.Nombre = productoBD.Nombre;
                producto.Descuento = productoBD.Descuento.ToString();

                db.RelCuentaProductos.Add(producto);
                var resU = db.SaveChanges();

                if (resU > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = producto.Id, message = "Producto Agregado." };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se pudo agregar el producto a la cuenta." };
            }
        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {
            using (var db = new restauranteContext())
            {
                var regitro = await db.RelCuentaProductos.FindAsync(id);
                //var regitro = db.RelCuentaProductos.Where(u => u.Id == id).First<RelCuentaProducto>();
                db.RelCuentaProductos.Remove(regitro);

                var result = await db.SaveChangesAsync();
                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue eliminado exitosamente." };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser eliminado." };
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
                                await _daoPro.ControlStock(producto.IdProducto.Value, Convert.ToInt32(producto.Cantidad));
                        }
                        catch (Exception ex) { error = error + " ControlStock: " + ex.Message; }
                        await StatusCuenta(venta.IdCuenta);

                        await _daoMesa.StatusOcupada(new Mesa { Id = idMesa, Ocupada = false });
                        return new ResponseModel { responseCode = 200, objectResponse = resU, message = "Venta regitrada exitosamente." + "\n" + error };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se encontraron ventas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> StatusCuenta(int id)
        {
            using (var db = new restauranteContext())
            {
                var regitro = await db.Cuentas.FindAsync(id);
                //var regitro = db.Cuentas.Where(u => u.Id == idCuenta).First<Cuenta>();
                regitro.CuentaActiva = false;

                var result = await db.SaveChangesAsync();

                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "La venta ha sido registrada" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
            }
        }

        public async Task<ResponseModel> GetAll(bool estaActiva)
        {
            var cuentas = new List<Cuenta>();
            using (var db = new restauranteContext())
            {
                cuentas = await db.Cuentas
                               .Include(c => c.RelCuentaProductos)
                                   .ThenInclude(r => r.IdProductoNavigation)
                               .Where(c => c.CuentaActiva.Value == estaActiva &&
                                           (c.IdCorte == null || c.IdCorte == 0))
                               .ToListAsync();
            }

            if (!cuentas.Any())
                return new ResponseModel { responseCode = 404, objectResponse = new List<Cuenta>(), message = "No se encontraron cuentas." };

            // Calcular el total y asignar el nombre del empleado
            Parallel.ForEach(cuentas, cuenta =>
            {
                cuenta.NombreEmpleado = cuenta.IdEmpleado.ToString();
                cuenta.total = cuenta.RelCuentaProductos.Sum(p => Convert.ToDecimal(p.Precio) * Convert.ToDecimal(p.Cantidad));
            });

            return new ResponseModel { responseCode = 200, objectResponse = cuentas, message = "Success" };
        }

        public async Task<ResponseModel> GetById(int id)
        {
            Cuenta cuenta = new Cuenta();
            using (var db = new restauranteContext())
            {
                cuenta = await db.Cuentas
                             .Include(c => c.RelCuentaProductos)
                                 .ThenInclude(r => r.IdProductoNavigation)
                             .Where(c => (c.IdCorte == null || c.IdCorte == 0))
                             .FirstAsync();
            }

            if (cuenta == null)
                return new ResponseModel { responseCode = 404, objectResponse = new List<Cuenta>(), message = "No se encontró la cuenta." };

            cuenta.NombreEmpleado = cuenta.IdEmpleado.ToString();
            cuenta.total = cuenta.RelCuentaProductos.Sum(p => Convert.ToDecimal(p.Precio) * Convert.ToDecimal(p.Cantidad));

            if (cuenta.Id > 0)
                return new ResponseModel { responseCode = 200, objectResponse = cuenta, message = "Success" };
            else
                return new ResponseModel { responseCode = 404, objectResponse = new Cuenta(), message = "No se encontraron cuentas." };
        }

        public async Task<bool> Exist(int id)
        {
            Cuenta cuenta = new Cuenta();
            using (var db = new restauranteContext())
            {
                cuenta = await db.Cuentas.FindAsync(id);
            }

            if (cuenta.Id > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}