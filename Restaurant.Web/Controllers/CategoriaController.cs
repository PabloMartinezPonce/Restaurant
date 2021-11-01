using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;
using Restaurante.Model;

namespace Restaurant.Web.Controllers
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
                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormCategoria(int id)
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
        public async Task<ResponseModel> CreateCategoria(Categoria usuario)
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
        public async Task<ResponseModel> EditCategoria(Categoria usuario)
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
        public async Task<ResponseModel> DeleteCategoria(int id)
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