using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using Restaurant.Model;
using System;

namespace Restaurante.Data.DAO
{
    public class VentasDAO
    {
        #region CRUD Entidad Venta

        public async Task<ResponseModel> Get(DateTime fechaInicio, DateTime fechaFin, int? idCorte)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    //var ventas = await db.Ventas.Include("IdCuentaNavigation").AsNoTracking()
                    //                            .Where(asd => asd.Fecha.Value.Date >= fechaInicio.Date && asd.Fecha.Value.Date <= fechaFin.Date)
                    //                            .ToListAsync();
                    var ventas = await db.Ventas.Include("IdCuentaNavigation").AsNoTracking()
                                .Where(asd => asd.IdCorte == idCorte || asd.IdCorte == 0)
                                .ToListAsync();

                    decimal totalEfectivo = 0;
                    decimal totalTarjeta = 0;
                    decimal propina = 0;
                    foreach (var venta in ventas)
                    {
                        propina = propina + venta.Propina.Value;
                        if (venta.Metodopago == "Tarjeta")
                            totalTarjeta = totalTarjeta + venta.Total.Value;
                        else
                            totalEfectivo = totalEfectivo + venta.Total.Value;
                    }

                    if (ventas.Count() >= 1)
                    {
                        ventas.Last().PropinaDiaria = propina;
                        ventas.Last().TotalTarjeta = totalTarjeta;
                        ventas.Last().TotalEfectivo = totalEfectivo;
                        return new ResponseModel { responseCode = 200, objectResponse = ventas, message = "Éxito" };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Venta>(), message = "No se encontraron ventas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Venta>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetForCorte(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var ventas = await db.Ventas.Include("IdCuentaNavigation").AsNoTracking()
                                                .Where(asd => asd.IdCorte == null || asd.IdCorte == 0).ToListAsync();
                    decimal totalEfectivo = 0;
                    decimal totalTarjeta = 0;
                    decimal propina = 0;
                    foreach (var venta in ventas)
                    {
                        propina = propina + venta.Propina.Value;
                        if (venta.Metodopago == "Tarjeta")
                            totalTarjeta = totalTarjeta + venta.Total.Value;
                        else
                            totalEfectivo = totalEfectivo + venta.Total.Value;
                    }

                    if (ventas.Count() >= 1)
                    {
                        ventas.Last().PropinaDiaria = propina;
                        ventas.Last().TotalTarjeta = totalTarjeta;
                        ventas.Last().TotalEfectivo = totalEfectivo;
                        return new ResponseModel { responseCode = 200, objectResponse = ventas, message = "Éxito" };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Venta>(), message = "No se encontraron ventas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Venta>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var venta = await db.Ventas.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (venta.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = venta, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "No se encontraron ventas." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetProductDetails(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var item = new EstadisticaProductoModel();
                    var productos = await db.RelCuentaProductos.Include("IdProductoNavigation").Where(pro => pro.IdProducto == id)
                           .Select(pro => new EstadisticaProductoModel
                           {
                               TotalCantidadVentidos = 1,
                               TotalVenta = Convert.ToDecimal(pro.Precio),
                               TotalCosto = Convert.ToDecimal(pro.IdProductoNavigation.PrecioCosto),
                           }).ToListAsync();

                    int cantidad = productos.Count();
                    decimal venta = Convert.ToDecimal(productos.First().TotalVenta) * cantidad;
                    decimal costo = Convert.ToDecimal(productos.First().TotalCosto) * cantidad;
                    item.TotalCantidadVentidos = productos.Count();
                    item.TotalVenta = Math.Round(venta, 2, MidpointRounding.AwayFromZero);
                    item.TotalCosto = Math.Round(costo, 2, MidpointRounding.AwayFromZero); ;

                    if (item != null)
                        return new ResponseModel { responseCode = 200, objectResponse = item, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Producto(), message = "El producto no existe." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Producto(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Venta regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Ventas.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Venta registrada exitosamente" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La venta no pudo ser registrada" };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Venta>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var regitro = db.Ventas.Where(u => u.Id == id).First<Venta>();
                    db.Ventas.Remove(regitro);

                    var result = await db.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "La venta no se reguistrada." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        #endregion
    }
}