using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class VentaController : Controller
    {
        private readonly VentasDAO _dao;
        public VentaController()
        {
            _dao = new VentasDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Ventas()
        {
            try
            {
                var result = await _dao.Get();
                var resultTotales = await _dao.Get();
                ViewBag.Total = resultTotales.objectResponse;

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