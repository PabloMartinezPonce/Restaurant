using Microsoft.EntityFrameworkCore;
using Restaurant.Web;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class VentasDAO
    {
        #region CRUD Entidad Venta

        public async Task<ResponseModel> Get()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var ventas = await db.Ventas.AsNoTracking().ToListAsync();

                    if (ventas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = ventas, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "No se encontraron ventas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
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
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Venta regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var listaAgrupada = new List<Venta>();

                    db.Ventas.Add(regitro);
                    var resU = await db.SaveChangesAsync();

                    if (resU > 0)
                    {
                        var listVentas = await db.Ventas.AsNoTracking().Where(e => e.IdCuenta == regitro.IdCuenta).ToListAsync();
                        foreach (var item in listVentas)
                        {
                            if (listaAgrupada.Any(a => a.IdProducto == item.IdProducto))
                            {
                                foreach (var item2 in listaAgrupada.Where(p => p.IdProducto == item.IdProducto))
                                    item2.Unidades = item2.Unidades + item.Unidades;
                            }
                            else
                            {
                                listaAgrupada.Add(new Venta
                                {
                                    Id = item.Id,
                                    PrecioVenta = item.PrecioVenta,
                                    Unidades = item.Unidades,
                                    Descuento = item.Descuento,
                                    IdProducto = item.IdProducto,
                                    IdCuenta = item.IdCuenta,
                                });
                            }
                        } 
                    }

                    if (listaAgrupada.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = listaAgrupada, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "La venta no se reguistrada." };
                }
            }
            catch (SqlException ex)
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
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        #endregion
    }
}