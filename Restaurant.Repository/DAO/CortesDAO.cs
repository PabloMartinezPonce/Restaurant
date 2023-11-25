using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Model;
using System;

namespace Restaurante.Data.DAO
{
    public class CortesDAO
    {
        #region CRUD Entidad Cortes

        public async Task<ResponseModel> GetAllCortes()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var cortes = await db.Cortes.AsNoTracking().ToListAsync();

                    if (cortes.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = cortes, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Corte>(), message = "No se encontraron cortes." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Corte>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var cortes = await db.Cortes.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (cortes.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = cortes.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Corte>(), message = "No se encontraron cortes." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Corte>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> ClearSales(int idCorte)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var ventas = con.Ventas.Where(f => f.IdCorte == null).ToList();
                    ventas.ForEach(a => a.IdCorte = idCorte);

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Las ventas se han actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "Las cuentas no se han actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> ClearAccount(int idCorte)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var cuentas = con.Cuentas.Where(f => f.IdCorte == null).ToList();
                    cuentas.ForEach(a => a.IdCorte = idCorte);

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Las cuentas se han actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "Las cuentas no se han actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> ClearCajaChica(int idCorte)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var cuentas = con.Cajachicas.Where(f => f.IdCorte == null).ToList();
                    cuentas.ForEach(a => a.IdCorte = idCorte);

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Las cuentas se han actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "Las cuentas no se han actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> FinishSales(Corte regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Cortes.OrderByDescending(p => p.Id).First<Corte>();
                    regitro.Observaciones = regitroView.Observaciones;
                    regitro.TotalEntradas = regitroView.TotalEntradas;
                    regitro.TotalPropinas = regitroView.TotalPropinas;
                    regitro.TotalSalidas = regitroView.TotalSalidas;
                    regitro.TotalVentasEfectivo = regitroView.TotalVentasEfectivo;
                    regitro.TotalVentasTarjeta = regitroView.TotalVentasTarjeta;
                    regitro.IdUsuario = regitroView.IdUsuario;
                    regitro.FechaCierre = GlobalConfig.GetMexDate();

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                    {
                        string error = string.Empty;
                        try { var resVentas = await ClearSales(regitro.Id); } catch (Exception ex) { error = error + " Venta: " + ex.Message + "\n"; }
                        try { var resCuentas = await ClearAccount(regitro.Id); } catch (Exception ex) { error = error + " Cuenta: " + ex.Message; }
                        try { var resCajas = await ClearCajaChica(regitro.Id); } catch (Exception ex) { error = error + " CajaChica: " + ex.Message; }
                        try { var resCorte = await Create(new Corte { FechaApertura = GlobalConfig.GetMexDate() }); } catch (Exception ex) { error = error + " Corte: " + ex.Message; }

                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El producto fue actualizado exitosamente." + "\n" + error };
                    }
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El producto no pudo ser actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Corte regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Cortes.Add(regitro);

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

        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var regitro = db.Cortes.Where(u => u.Id == id).First<Corte>();
                    db.Cortes.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no pudo ser eliminada." };
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