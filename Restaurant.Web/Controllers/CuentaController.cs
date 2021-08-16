using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Web.Common;

namespace LasPaseras.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class CuentaController : Controller
    {
        private readonly CuentasDAO _dao;

        public CuentaController()
        {
            _dao = new CuentasDAO();
        }

        //[HttpPost]
        //public ActionResult ValidaPedido(int idMesa, int unidades, string producto)
        //{
        //    try
        //    {
        //        var usuario = JsonSerializer.Deserialize<Usuario>(HttpContext.Session.GetString("UserSession"));
        //        var pedido = new Cuenta { IdMesa = idMesa, IdEmpleado = usuario.Id, NombreProducto = producto, UnidadesProducto = unidades };

        //        var result = _dao.ValidaPedido(pedido);
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(CommonTxt.GetResponseError(ex));
        //    }
        //}

    }
}