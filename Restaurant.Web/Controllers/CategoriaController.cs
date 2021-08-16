using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;

namespace LasPaseras.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class CategoriaController : Controller
    {
        private readonly CategoriasDAO _dao;
        public CategoriaController()
        {
            _dao = new CategoriasDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Categorias()
        {
            try
            {
                var result = await _dao.GetAllCategorias();
                return View(result);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> CategoriasById(int id)
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
        public async Task<ActionResult> CreateCategoria(Categoria usuario)
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

        [HttpPost]
        public ActionResult EditCategoria(Categoria usuario)
        {
            try
            {
                var result = _dao.Update(usuario);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpPost]
        public ActionResult DeleteCategoria(int id)
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