using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;
using Restaurante.Model;
using Restaurant.Model;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class IngredienteController : Controller
    {
        private readonly IngredientesDAO _dao;
        private readonly MedicionesDAO _daoMed;
        public IngredienteController()
        {
            _dao = new IngredientesDAO();
            _daoMed = new MedicionesDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Ingredientes(bool? buscarActivos)
        {
            try
            {
                var result = await _dao.GetAll(buscarActivos);
                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormIngrediente(int id)
        {
            try
            {
                var result = await _dao.GetById(id);

                var resultTC = await _daoMed.GetAll(true);
                ViewBag.ListaCategorias = resultTC.objectResponse;

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> IngredientesById(int id)
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
        public async Task<ResponseModel> EditarIngrediente(Ingrediente item)
        {
            try
            {
                var result = await _dao.Update(item);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CreateIngrediente(Ingrediente item)
        {
            try
            {
                var result = await _dao.Create(item);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> DeleteIngrediente(int id)
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

    }
}