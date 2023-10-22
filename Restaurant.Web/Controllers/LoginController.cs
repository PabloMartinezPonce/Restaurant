using Microsoft.AspNetCore.Mvc;
using Restaurant.Web.Common;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Restaurante.Web.Common;
using Restaurante.Data.DBModels;
using Restaurante.Data.DAO;

namespace Restaurant.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginDAO _daoLogin;
        private readonly ConfiguracionDAO _daoConfig;

        public LoginController()
        {
            _daoLogin = new LoginDAO();
            _daoConfig = new ConfiguracionDAO();
        }

        public async Task<ActionResult> Login()
        {
            var result = await _daoConfig.GetById(4);
            ViewBag.fondoLogin = result.objectResponse;

            var result2 = await _daoConfig.GetById(2);
            ViewBag.logoLogin = result2.objectResponse;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidateLogin(string usuario, string contrasena)
        {
            try
            {
                var result = await _daoLogin.ValidateLogin(usuario, contrasena);

                if (result.responseCode == 200)
                {
                    string user = JsonSerializer.Serialize(result.objectResponse);
                    HttpContext.Session.SetString("UserSession", user);
                }

                var result2 = await _daoConfig.GetAll();
                string configuraciones = JsonSerializer.Serialize(result2.objectResponse);
                HttpContext.Session.SetString("ConfigSession", configuraciones);

                string jsonString = JsonSerializer.Serialize(result);

                return Content(jsonString);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> DisableAccount(string usuario)
        //{
        //    try
        //    {
        //        var result = await _daoLogin.DisableAccount(usuario, QueriesTxt.userSelectByUser);
        //        var resString = JsonSerializer.Serialize(result);

        //        return Content(resString);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(CommonTxt.GetMessage(ex));
        //    }
        //}

        public ActionResult LogOff()
        {
            var usuario = JsonSerializer.Deserialize<Usuario>(HttpContext.Session.GetString("UserSession"));
            HttpContext.Session.Remove("UserSession");
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }

        [Permiso(permiso = EnumPermisos.MisGastos)]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }

        public ActionResult PermisoDenegado()
        {
            return View();
        }

        public ActionResult SesionExpirada()
        {
            return View();
        }

    }
}