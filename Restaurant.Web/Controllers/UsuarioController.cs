﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Http;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class UsuarioController : Controller
    {
        private readonly IUsuariosDAO _daoUsuario;
        private readonly IConfiguracionDAO _daoConfig;
        private readonly RolesDAO _daoRoles;
        public UsuarioController(IUsuariosDAO daoUsuario, IConfiguracionDAO daoConfig)
        {
            _daoUsuario = daoUsuario;
            _daoConfig = daoConfig;
            _daoRoles = new RolesDAO();
        }

        [HttpGet]
        public ActionResult MiPerfil()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Staff()
        {
            try
            {
                List<UsuarioDTO> usuarios = new List<UsuarioDTO>();
                var result = await _daoUsuario.GetUsers();

                if (result.responseCode == 200)
                    usuarios = JsonSerializer.Serialize<List<UsuarioDTO>>(result.objectResponse);

                return View(usuarios);
            }
            catch (Exception ex)
            {
                return View(CommonTxt.GetMessage(ex));
            }
        }

        [HttpGet]
        public async Task<ActionResult> Usuarios()
        {
            try
            {
                var result = await _daoUsuario.GetUsers();

                if (result.responseCode == 200)
                {
                    var roles = await _daoRoles.GetRoles();
                    ViewBag.listRoles = roles.objectResponse;
                }
                else
                {
                    ViewBag.listRoles = new List<RolDTO>();
                }

                return View(result.objectResponse);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(Usuario usuario)
        {
            try
            {
                var result = await _daoUsuario.CreateUser(usuario);

                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(Usuario usuario)
        {
            try
            {
                var result = await _daoUsuario.UpdateUser(usuario);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _daoUsuario.DeleteUser(id);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }

        [HttpGet]
        public async Task<ResponseModel> DarkMode(bool active)
        {
            try
            {
                var result = await _daoConfig.ActiveDarkMode(active);

                var result2 = await _daoConfig.GetAll();
                string configuraciones = JsonSerializer.Serialize(result2.objectResponse);
                HttpContext.Session.SetString("ConfigSession", configuraciones);

                return result;
            }
            catch (Exception ex)
            {
                return CommonTxt.GetResponseError(ex);
            }
        }

        [HttpPost]
        public ActionResult ModifyStatus(string userName, bool estatus)
        {
            try
            {
                var result = _daoUsuario.ModifyStatus(userName, estatus);
                return Json(result, 0);
            }
            catch (Exception ex)
            {
                return Json(CommonTxt.GetResponseError(ex));
            }
        }
    }
}
