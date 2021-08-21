using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Web;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Data.DBModels;
using Restaurante.Model;
using Restaurante.Web.Common;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class CuentaController : Controller
    {
        private readonly CuentasDAO _dao;
        private readonly UsuarioDAO _daoUsr;
        private readonly MesasDAO _daoMsa;
        private readonly ProductosDAO _daoPro;

        public CuentaController()
        {
            _dao = new CuentasDAO();
            _daoUsr = new UsuarioDAO();
            _daoMsa = new MesasDAO();
            _daoPro = new ProductosDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Cuentas(int idEmpleado)
        {
            try
            {
                ResponseModel result = new ResponseModel();
                if (idEmpleado == 0)
                    result = await _dao.GetAll();
                else
                    result = await _dao.GetCuentaByIdEmpleado(idEmpleado);

                var resultCom = await _daoUsr.GetUserByRol(3);
                ViewBag.ListaEmpleados = resultCom.objectResponse;

                var resultMesa = await _daoMsa.Get();
                ViewBag.ListaMesas = resultMesa.objectResponse;

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Content(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormCuenta(int id)
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
        public async Task<ActionResult> FormAgregarProducto(int id)
        {
            try
            {
                var result = await _dao.GetById(id);

                var resultProductos = await _daoPro.Get();
                ViewBag.ListaProductos = resultProductos.objectResponse;

                //var resultProductos = await _daoPro.Get();
                //List<Producto> list = resultProductos.objectResponse;
                //ViewBag.ListaProductos = list
                //                         .GroupBy(u => u.IdCategoria)
                //                         .Select(grp => grp.ToList())
                //                         .ToList();

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormPagar(int id)
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
        public async Task<ActionResult> CuentaById(int id)
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
        public async Task<ResponseModel> CreateCuenta(Cuenta obj)
        {
            try
            {
                var result = await _dao.Create(obj);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }


        [HttpPost]
        public async Task<ResponseModel> AddProduct(RelCuentaProducto producto)
        {
            try
            {
                var result = await _dao.AddProducto(producto);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> PagarCuenta(Venta obj, int idMesa)
        {
            try
            {
                var result = await _dao.CreatePayment(obj, idMesa);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

    }
}