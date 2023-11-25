using Microsoft.AspNetCore.Mvc;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Model;
using Restaurante.Web.Common;
using System;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class ComplementoController : Controller
    {
        private readonly IComplementosDAO _dao;
        public ComplementoController(IComplementosDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult> Complementos()
        {
            try
            {
                var productos = await _dao.GetAllComplementos();
                return View(productos.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormComplemento(int id)
        {
            try
            {
                var result = await _dao.GetById(id);

                var resultCat = await _dao.GetTipoComplementos();
                ViewBag.ListaTipos = resultCat.objectResponse;

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CrearComplemento(Complemento pedido)
        {
            try
            {
                var result = await _dao.Create(pedido);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> EditarComplemento(Complemento pedido)
        {
            try
            {
                var result = await _dao.Update(pedido);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> EliminarComplemento(int id)
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

        [HttpPost]
        public async Task<ResponseModel> CambiarEstatus(bool estatus, int Id)
        {
            try
            {
                var result = await _dao.SetActivity(estatus, Id);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

    }
}
