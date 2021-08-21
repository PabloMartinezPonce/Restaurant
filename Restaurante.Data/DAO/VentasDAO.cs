using Microsoft.EntityFrameworkCore;
using Restaurant.Web;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;

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
                    var ventas = await db.Ventas.Include("IdCuentaNavigation").AsNoTracking().ToListAsync();
                    decimal total = 0;
                    decimal propina = 0;
                    foreach (var venta in ventas)
                    {
                        total = venta.TotalDiaria + venta.Total.Value;
                        propina = venta.PropinaDiaria + venta.Propina.Value;
                        venta.TotalDiaria = total;
                        venta.PropinaDiaria = propina;
                    }

                    if (ventas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = ventas, message = "Éxito" };
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
                    db.Ventas.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Venta registrada exitosamente" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La venta no pudo ser registrada" };
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