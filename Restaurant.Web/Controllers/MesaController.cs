using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;
using Restaurante.Model;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class MesaController : Controller
    {
        private readonly IMesasDAO _dao;
        public MesaController(IMesasDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult> Mesas()
        {
            try
            {
                var result = await _dao.Get();
                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormMesa(int id)
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
        public async Task<ActionResult> MesasById(int id)
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
        public async Task<ResponseModel> CreateMesa(Mesa usuario)
        {
            try
            {
                var result = await _dao.Create(usuario);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> EditMesa(Mesa usuario)
        {
            try
            {
                var result = await _dao.Update(usuario);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> DeleteMesa(int id)
        {
            try
            {
                var result = await _dao.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        //[HttpPost]
        //public ActionResult ModifyStatus(string usuario, bool esCorreo, bool enable)
        //{
        //    try
        //    {
        //        var result = _dao.m(usuario, esCorreo, enable);
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { objectResponse = false, message = ex.Message }, 0);
        //    }
        //}

    }
}