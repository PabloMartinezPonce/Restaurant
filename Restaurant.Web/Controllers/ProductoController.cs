using Microsoft.AspNetCore.Mvc;
using Restaurant.Web.Common;
using Restaurante.Data.DAO;
using Restaurante.Model;
using Restaurante.Model.DTOs;
using Restaurante.Web.Common;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Restaurante.Data.DBModels;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class ProductoController : Controller
    {
        private readonly ProductosDAO _dao;
        private readonly CategoriasDAO _daoCat;
        private readonly ComplementosDAO _daoCom;
        private readonly ProveedoresDAO _daoPro;
        private readonly UsuarioDAO _daoUsr;
        public ProductoController()
        {
            _dao = new ProductosDAO();
            _daoCat = new CategoriasDAO();
            _daoCom = new ComplementosDAO();
            _daoPro = new ProveedoresDAO();
            _daoUsr = new UsuarioDAO();
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
                    productos = await _dao.Get();

                var resultCat = await _daoCat.GetAllCategorias();
                ViewBag.ListaCategorias = resultCat.objectResponse;

                var resultCom = await _daoCom.GetAllComplementos();
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

                var resultPC = await _dao.GetAsComplement();
                ViewBag.ListaProductos = resultPC.objectResponse;

                var resultCom = await _daoCom.GetAllTipos();
                List<Complemento> list = resultCom.objectResponse;
                ViewBag.ListaComplementos = list
                                         .GroupBy(u => u.IdTipoComplemento)
                                         .Select(grp => grp.ToList())
                                         .ToList();

                return View(productos.objectResponse);
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
                var resultCom = _daoCom.GetAllComplementos().Result;
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
