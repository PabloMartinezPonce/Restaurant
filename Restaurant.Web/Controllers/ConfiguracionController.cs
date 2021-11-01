using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DBModels;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;
using Restaurante.Model.Model;
using Restaurante.Model;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class ConfiguracionController : Controller
    {
        private readonly ConfiguracionDAO _dao;
        public ConfiguracionController()
        {
            _dao = new ConfiguracionDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Configuraciones()
        {
            try
            {
                var result = await _dao.GetAll();
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
        public async Task<ActionResult> ConfiguracionesById(int id)
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
        public async Task<ResponseModel> UpdateAyuda(ConfigModel config)
        {
            try
            {
                var result = await _dao.Update(config);
                if (result.responseCode == 200)
                {
                    var result2 = await _dao.GetAll();
                    string configuraciones = JsonSerializer.Serialize(result2.objectResponse);
                    HttpContext.Session.SetString("ConfigSession", configuraciones);
                }
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

    }
}