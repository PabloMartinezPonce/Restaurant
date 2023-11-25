using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Web.Common;
using Restaurant.Model;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class VentaController : Controller
    {
        private readonly IVentasDAO _dao;
        public VentaController(IVentasDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult> Ventas(string _fechaInicio, string _fechaFin)
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

                var result = await _dao.Get(fechaInicio, fechaFin, null);
                //var resultTotales = await _dao.Get();
                //ViewBag.Total = resultTotales.objectResponse;

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormVenta(int id)
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
        public async Task<ActionResult> VentasById(int id)
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
        public async Task<ActionResult> CreateVenta(Venta usuario)
        {
            try
            {
                var result = await _dao.Create(usuario);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

    }
}