using System.Data.SqlClient;
using System.Linq;
using System;
using Restaurant.Web;
using Microsoft.EntityFrameworkCore;
using Restaurante.Model;

namespace Restaurante.Data.DAO
{
    public class CuentasDAO
    {
        #region CRUD Entidad Cuenta

        public ResponseModel GetCuentaByIdMesa(Cuenta cuenta)
        {
            try
            {
                var venta = new Venta();
                using (var db = new restauranteContext())
                {
                   //Obtiene Cuenta
                    var qCuenta = from cta in db.Cuentas.AsNoTracking()
                                  where cta.IdMesa == cuenta.IdMesa && cta.CuentaActiva == true
                                  select new Cuenta
                                  {
                                      Id = cta.Id
                                  };
                    var cuentas = qCuenta.ToList();

                    //Obtiene Producto
                    var qProducto = from pto in db.Productos.AsNoTracking()
                                    where pto.Nombre.Contains(cuenta.NombreProducto)
                                    select new Producto
                                    {
                                        Id = pto.Id,
                                        PrecioVenta = pto.PrecioVenta
                                    };
                    var productos = qProducto.ToList();

                    if (productos.Count() >= 1)
                    {
                        venta.IdProducto = productos.First().Id;
                        venta.PrecioVenta = productos.First().PrecioVenta;
                        venta.Unidades = cuenta.UnidadesProducto;

                        if (cuentas.Count() >= 1)
                        {
                            venta.IdCuenta = cuentas.First().Id;
                        }
                        else
                        {
                            var _cuenta = new Cuenta
                            {
                                CuentaActiva = true,
                                FechaApertura = DateTime.Now,
                                FechaCierre = DateTime.Now,
                                IdMesa = cuenta.IdMesa,
                                IdEmpleado = cuenta.IdEmpleado,
                            };
                            venta.IdCuenta = Create(_cuenta).objectResponse;
                        }
                    }
                    else
                    {
                        venta = null;
                    }

                    if (venta != null)
                        return new ResponseModel { responseCode = 200, objectResponse = venta, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se encontraron ventas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public ResponseModel Create(Cuenta cuentas)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Cuentas.Add(cuentas);
                    var resU = db.SaveChanges();

                    if (resU > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = cuentas.Id, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se encontraron ventas." };
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