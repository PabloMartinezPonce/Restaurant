﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Web;
using Restaurante.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data.DAO
{
    public class MesasDAO
    {
        #region CRUD Entidad Mesas

        public async Task<ResponseModel> Get()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var mesas = await db.Mesas.AsNoTracking().ToListAsync();

                    if (mesas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = mesas, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Mesa(), message = "No se encontraron mesas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var mesas = await db.Mesas.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (mesas.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = mesas.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Mesa(), message = "No se encontraron mesas." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = ex, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Mesa regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    db.Mesas.Add(regitro);
                    var result = await db.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Mesa regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Mesas.Where(u => u.Id == regitroView.Id).First<Mesa>();
                    if (!string.IsNullOrEmpty(regitroView.Mesa1)) regitro.Mesa1 = regitroView.Mesa1;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;

                    var result = await con.SaveChangesAsync();

                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
                }
            }
            catch (SqlException ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var regitro = db.Mesas.Where(u => u.Id == id).First<Mesa>();
                    db.Mesas.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser eliminada." };
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