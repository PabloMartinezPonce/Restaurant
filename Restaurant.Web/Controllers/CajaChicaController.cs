using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Data.DBModels;
using Restaurante.Web.Common;
using Restaurante.Model;
using Restaurant.Model;
using Restaurant.Repository.Interfaces;

namespace Restaurant.Web.Controllers
{
    [Permiso(permiso = EnumPermisos.AdministradorTotal)]
    public class CajaChicaController : Controller
    {
        private readonly ICajaChicaDAO _dao;

        public CajaChicaController(ICajaChicaDAO dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult> CajaChicas()
        {
            var result = await _dao.GetAll(GlobalConfig.GetMexDate());
            return View(result.objectResponse);
        }

        [HttpGet]
        public async Task<ActionResult> FormCajaChica(int id)
        {
            var result = await _dao.GetById(id);
            return View(result.objectResponse);
        }

        [HttpGet]
        public async Task<ActionResult> CajaChicasById(int id)
        {
            var result = await _dao.GetById(id);
            return View(result);
        }

        [HttpPost]
        public async Task<ResponseModel> CreateCajaChica(CajachicaDTO entityDTO)
        {
            var result = await _dao.Create(entityDTO);
            return result;
        }

        [HttpPost]
        public async Task<ResponseModel> DeleteCajaChica(int id)
        {
            var result = await _dao.Delete(id);
            return result;
        }
    }
}