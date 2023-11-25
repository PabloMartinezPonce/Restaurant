using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Restaurant.Web.Controllers;
using Restaurante.Data.DBModels;
using Restaurante.Model;
using System;
using System.Linq;
using System.Text.Json;

namespace Restaurante.Web.Common
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        public EnumPermisos permiso { get; set; }
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public PermisoAttribute(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                var strSession = filterContext.HttpContext.Session.GetString("UserSession");
                var usuario = JsonSerializer.Deserialize<Usuario>(strSession);
                if (usuario == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "Login"
                    }));
                }
                //else if (!usuario.Roles.Permisos.Any(p => p.id == (int)permiso))
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //    {
                //        controller = "Login",
                //        action = "PermisoDenegado"
                //    }));
                //}
            }
            catch (Exception)
            {
                //MvcApplication.log.Error(ex.Message);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "Login"
                }));
            }
        }
    }
}