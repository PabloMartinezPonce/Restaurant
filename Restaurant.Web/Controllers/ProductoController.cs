using Microsoft.AspNetCore.Mvc;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Model;
using Restaurante.Web.Common;
using System;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using Microsoft.AspNetCore.Http;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class ProductoController : Controller
    {
        private readonly IProductosDAO _dao;
        private readonly IComplementosDAO _daoCom;
        private readonly IUsuariosDAO _daoUsr;
        private readonly ICategoriasDAO _daoCat;
        private readonly IVentasDAO _daoVenta;
        private readonly ProveedoresDAO _daoPro;
        private readonly MedicionesDAO _daoMed;
        private readonly IngredientesDAO _daoIng;
        public ProductoController(IProductosDAO dao, IComplementosDAO daoCom, IUsuariosDAO daoUsr, ICategoriasDAO daoCat, IVentasDAO daoVenta)
        {
            _dao = dao;
            _daoCom = daoCom;
            _daoUsr = daoUsr;
            _daoCat= daoCat;
            _daoPro = new ProveedoresDAO();
            _daoVenta = daoVenta;
            _daoMed = new MedicionesDAO();
            _daoIng = new IngredientesDAO();
        }

        [HttpGet]
        public async Task<ActionResult> Productos(int id, string filtro)
        {
            try
            {
                ResponseModel productos = new ResponseModel();

                if (filtro == "Categoria")
                    productos = await _dao.GetByFilter(id, filtro);
                else if (filtro == "Complemento")
                    productos = await _dao.GetByFilter(id, filtro);
                else
                    productos = await _dao.GetAll();

                var resultCat = await _daoCat.GetAllCategorias();
                ViewBag.ListaCategorias = resultCat.objectResponse;

                var resultCom = await _daoCom.GetAllActiveComplementos();
                ViewBag.ListaCatComplemento = resultCom.objectResponse;

                return View(productos.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> FormProducto(int id)
        {
            try
            {
                var productos = await _dao.GetById(id);

                var resultCat = await _daoCat.GetAllCategorias();
                ViewBag.ListaCategorias = resultCat.objectResponse;

                var resultPro = await _daoPro.GetAllProveedores();
                ViewBag.ListaProveedores = resultPro.objectResponse;

                var resultTC = await _daoCom.GetTipoComplementos();
                ViewBag.ListaTiposCom = resultTC.objectResponse;

                var resultMe = await _daoMed.GetAll(true);
                ViewBag.ListaMedicion = resultMe.objectResponse;
                
                var resultIn = await _daoIng.GetAll(true);
                ViewBag.ListaIdIngredientes = resultIn.objectResponse;

                //var resultPC = await _dao.GetAsComplement();
                //ViewBag.ListaProductos = resultPC.objectResponse;

                //var resultCom = await _daoCom.GetAllTipos();
                //List<Complemento> list = resultCom.objectResponse;
                //ViewBag.ListaComplementos = list
                //                         .GroupBy(u => u.IdTipoComplemento)
                //                         .Select(grp => grp.ToList())
                //                         .ToList();

                return View(productos.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> DetalleProducto(int id)
        {
            try
            {
                var producto = await _dao.GetById(id);

                var details = await _daoVenta.GetProductDetails(id);
                ViewBag.ProductDetail = details.objectResponse;

                int idComplemento = producto.objectResponse.IdTipoComplemento ?? 0;
                var resultCom = await _daoCom.GetAllActiveComplementos(idComplemento);
                ViewBag.ListaComplementos = resultCom.objectResponse;

                return View(producto.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CrearProducto(Producto pedido)
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
        public async Task<ResponseModel> AgregarIngrediente(RelProductoRecetum ing)
        {
            try
            {
                var result = await _dao.AddIngredient(ing);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> EliminarIngrediente(int id)
        {
            try
            {
                var result = await _dao.DeleteIngredient(id);
                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public async Task<ResponseModel> EditarProducto(Producto pedido)
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

        [HttpPost]
        public async Task<ResponseModel> EliminarProducto(int id)
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
        public ActionResult Productos(IFormFile postedFile, int id)
        {
            try
            {
                var result = FTP.FtpUploadAsync(postedFile);
                ResponseModel resultDB = new ResponseModel();
                if (result.blobExist)
                {
                    resultDB = _daoUsr.UpdateImage(id, postedFile.FileName).Result;
                }

                //View Products
                var productos = _dao.Get().Result;
                var resultCat = _daoCat.GetAllCategorias().Result;
                ViewBag.ListaCategorias = resultCat.objectResponse;
                var resultCom = _daoCom.GetAllActiveComplementos().Result;
                ViewBag.ListaCatComplemento = resultCom.objectResponse;

                return View(productos.objectResponse);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

    }
}
