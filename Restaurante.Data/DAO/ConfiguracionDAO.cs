using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Restaurante.Model.Model;
using System;

namespace Restaurante.Data.DAO
{
    public class ConfiguracionDAO
    {
        #region CRUD Entidad ConfiguracionesSistema

        public async Task<ResponseModel> GetAll()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var ConfiguracionesSistema = await db.Configuracionsistemas.AsNoTracking().ToListAsync();

                    if (ConfiguracionesSistema.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = ConfiguracionesSistema, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Configuracionsistema>(), message = "No se encontraron configuraciones." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Configuracionsistema>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var ConfiguracionesSistema = await db.Configuracionsistemas.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (ConfiguracionesSistema.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = ConfiguracionesSistema.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Configuracionsistema>(), message = "No se encontraron configuraciones." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Configuracionsistema>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(ConfigModel config)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    int id = Convert.ToInt16(config.name);
                    var registro = db.Configuracionsistemas.Where(u => u.Id == id).First<Configuracionsistema>();
                    if (!string.IsNullOrEmpty(config.value)) registro.Valor = config.value;

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "La configuración fue actualizada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La configuración no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> ActiveDarkMode(bool active)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var registro = db.Configuracionsistemas.Where(u => u.Id == 1).First<Configuracionsistema>();
                    registro.Valor = !active ? "dark-skin" : string.Empty;

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "La configuración fue actualizada exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La configuración no existe." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }
        #endregion
    }
}