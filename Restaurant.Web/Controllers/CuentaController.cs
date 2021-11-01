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
        private readonly CategoriasDAO _daoCat;
        private readonly UsuarioDAO _daoUsr;
        private readonly MesasDAO _daoMsa;
        private readonly ProductosDAO _daoPro;
        private readonly ComplementosDAO _daoCom;

        public CuentaController()
        {
            _dao = new CuentasDAO();
            _daoCat = new CategoriasDAO();
            _daoUsr = new UsuarioDAO();
            _daoMsa = new MesasDAO();
            _daoPro = new ProductosDAO();
            _daoCom = new ComplementosDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Cuentas(int idEmpleado, bool estaActiva = true)
        {
            try
            {
                ResponseModel result = new ResponseModel();
                if (idEmpleado == 0)
                    result = await _dao.GetAll(estaActiva);
                else
                    result = await _dao.GetCuentaByIdEmpleado(idEmpleado, estaActiva);

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

                var resultCat = await _daoCat.GetAllCategorias();
                ViewBag.ListaCategorias = resultCat.objectResponse;

                var resultCom = await _daoCom.GetAllActiveComplementos();
                ViewBag.ListaComplementos = resultCom.objectResponse;

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

                Cuenta cuenta = result.objectResponse;
                var lista = cuenta.RelCuentaProductos
                                         .GroupBy(u => u.IdProducto)
                                         .Select(grp => grp.ToList())
                                         .ToList();
                ViewBag.ListaVentas = lista;

                return View(cuenta);
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
        public async Task<ResponseModel> CancelarCuenta(int id)
        {
            try
            {
                var result = await _dao.Cancel(id);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CreateCuentaAssistent(Cuenta obj)
        {
            try
            {
                var result = new ResponseModel();
                var resultExist = await _daoMsa.GetById(obj.IdMesa);
                Mesa table = resultExist.objectResponse;

                if (table.Ocupada.Value)
                    result = new ResponseModel { responseCode = 400, message = "Ya existe una cuenta abierta para la mesa " + obj.IdMesa, objectResponse = false };
                else
                    result = await _dao.Create(obj);

                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddProductAssistent(RelCuentaProducto producto)
        {
            try
            {
                var exist = await _dao.Exist(producto.IdCuenta.Value);
                if (exist)
                {
                    var resultPro = await _daoPro.GetByName(producto.Nombre);
                    Producto productoBD = resultPro.objectResponse;
                    producto.Precio = productoBD.PrecioVenta.ToString();
                    producto.Nombre = productoBD.Nombre;
                    producto.Descuento = productoBD.Descuento.ToString();
                    var result = await _dao.AddProducto(producto);

                    return result;
                }
                else
                    return CommonTxt.GetNewResponse(404, "La cuenta no existe, porfavor crea una nueva cuenta.", false);
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddProductsAssistent(List<RelCuentaProducto> productos)
        {
            try
            {
                var exist = await _dao.Exist(productos[0].IdCuenta.Value);
                if (exist)
                {
                    foreach (var producto in productos)
                    {
                        var resultPro = await _daoPro.GetByName(producto.Nombre);
                        Producto productoBD = resultPro.objectResponse;
                        producto.Precio = productoBD.PrecioVenta.ToString();
                        producto.Nombre = productoBD.Nombre;
                        producto.Descuento = productoBD.Descuento.ToString();
                        var result = await _dao.AddProducto(producto);
                    }
                    return CommonTxt.GetNewResponse(200, "Se han agregado " + productos.Count + " productos a la cuenta " + productos[0].IdCuenta.Value, true);
                }
                else
                    return CommonTxt.GetNewResponse(404, "La cuenta no existe, porfavor crea una nueva cuenta.", false);
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
        public async Task<ResponseModel> DeleteProduct(int idRel)
        {
            try
            {
                var result = await _dao.DeleteProduct(idRel);
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