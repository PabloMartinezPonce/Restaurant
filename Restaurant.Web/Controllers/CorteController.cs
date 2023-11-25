using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;
using Restaurant.Model;
using Restaurante.Model;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class CorteController : Controller
    {
        private readonly CortesDAO _dao;
        private readonly VentasDAO _daoVen;
        private readonly ICajaChicaDAO _daoCaja;
        public CorteController(ICajaChicaDAO daoCaja)
        {
            _dao = new CortesDAO();
            _daoVen = new VentasDAO();
            _daoCaja = daoCaja;
        }

        [HttpGet]
        public async Task<ActionResult> CorteDiario(DateTime _fechaInicio, DateTime _fechaFin)
        {
            try
            {
                var fechaInicio = Convert.ToDateTime(_fechaInicio);
                var fechaFin = Convert.ToDateTime(_fechaFin);

                if (fechaInicio.Year == 0001 || fechaFin.Year == 0001)
                {
                    fechaInicio = GlobalConfig.GetMexDate();
                    fechaFin = GlobalConfig.GetMexDate();
                }

                var result = await _daoVen.GetForCorte(fechaInicio, fechaFin);

                var result2 = await _daoCaja.GetTotales(fechaInicio);
                ViewBag.CajaChica = result2.objectResponse;

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Cortes(DateTime _fechaInicio, DateTime _fechaFin)
        {
            try
            {
                var fechaInicio = Convert.ToDateTime(_fechaInicio);
                var fechaFin = Convert.ToDateTime(_fechaFin);

                if (fechaInicio.Year == 0001 || fechaFin.Year == 0001)
                {
                    fechaInicio = GlobalConfig.GetMexDate();
                    fechaFin = GlobalConfig.GetMexDate();
                }

                var result = await _dao.GetAllCortes();

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormCorte(int id)
        {
            try
            {
                var result = await _dao.GetById(id);

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> CortesById(int id)
        {
            try
            {
                var result = await _dao.GetById(id);
                return View(result);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CreateCorte(Corte usuario)
        {
            try
            {
                var result = await _dao.FinishSales(usuario);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public ActionResult DeleteCorte(int id)
        {
            try
            {
                var result = _dao.Delete(id);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { objectResponse = false, message = ex.Message }, 0);
            }
        }

    }
}