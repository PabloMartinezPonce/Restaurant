using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Restaurant.Web.Controllers;
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
                //var stringSession = HttpContextAccessor.HttpContext.Session.GetString("UserSession");
                base.OnActionExecuting(filterContext);
                //var usuario = JsonSerializer.Deserialize<UsuarioDTO>(stringSession);
                var usuario = new UsuarioDTO();
                if (usuario == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Login",
                        action = "AccesoDenegado"
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
            catch (Exception ex)
            {
                //MvcApplication.log.Error(ex.Message);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "AccesoDenegado"
                }));
            }
        }
    }
}