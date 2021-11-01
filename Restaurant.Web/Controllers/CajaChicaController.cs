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
    public class CajaChicaController : Controller
    {
        private readonly CajaChicaDAO _dao;
        public CajaChicaController()
        {
            _dao = new CajaChicaDAO();
        }

        [HttpGet]
        public async Task<ActionResult> CajaChicas()
        {
            try
            {
                var result = await _dao.GetAll(GlobalConfig.GetMexDate());
                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormCajaChica(int id)
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
        public async Task<ActionResult> CajaChicasById(int id)
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
        public async Task<ResponseModel> CreateCajaChica(Cajachica usuario)
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
        public async Task<ResponseModel> DeleteCajaChica(int id)
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