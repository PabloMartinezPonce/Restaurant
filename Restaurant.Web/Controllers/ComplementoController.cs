using Microsoft.AspNetCore.Mvc;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Model;
using Restaurante.Model.DTOs;
using Restaurante.Web.Common;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class ComplementoController : Controller
    {
        private readonly ComplementosDAO _dao;
        public ComplementoController()
        {
            _dao = new ComplementosDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Complementos()
        {
            try
            {
                var productos = await _dao.GetAllComplementos();
                return View(productos.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormComplemento(int id)
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

        [HttpPost]
        public async Task<ActionResult> CrearComplemento(Complemento pedido)
        {
            try
            {
                var result = await _dao.Create(pedido);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditarComplemento(Complemento pedido)
        {
            try
            {
                var result = await _dao.Update(pedido);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> CambiarEstatus(Complemento pedido)
        //{
        //    try
        //    {
        //        var result = _dao.CambiarEstatus(pedido, Security.GetUserToken());
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //           return Json(CommonTxt.GetResponseError(ex));
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> EliminarComplemento(int id)
        {
            try
            {
                var result = await _dao.Delete(id);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

    }
}
