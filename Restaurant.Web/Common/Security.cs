using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Model;

namespace Restaurante.Web.Common
{
    public class Security : Controller
    {
        private string session = string.Empty;
        IHttpContextAccessor httpContextAccessor;
        public Security()
        {
            //session = httpContextAccessor.HttpContext.Session.GetString("UserSession");
        }

        //public string GetUserToken()
        //{
        //    try
        //    {
        //        var user = JsonSerializer.Deserialize<UsuarioDTO>(HttpContext.Session.GetString("UserSession"));
        //        if (user != null)
        //            return user.token;
        //        else
        //            return string.Empty;
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}

        //public UsuarioDTO GetUserSession()
        //{
        //    try
        //    {
        //        var user = JsonSerializer.Deserialize<UsuarioDTO>(session);
        //        if (user != null)
        //            return user;
        //        else
        //            return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}