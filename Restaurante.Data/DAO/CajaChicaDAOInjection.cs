using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Restaurante.Data.DAO
{
    public class CajaChicaDAOInjection
    {
        public readonly restauranteContext db;
        public CajaChicaDAOInjection(restauranteContext context) { db = context; }

        //#region CRUD Entidad CajaChica

        //public async Task<ResponseModel> GetAll(DateTime fechaInicio)
        //{
        //    try
        //    {
        //        var cajaChica = await db.Cajachicas.AsNoTracking().Where(asd => asd.IdCorte == null || asd.IdCorte == 0).ToListAsync();

        //        if (cajaChica.Count() >= 1)
        //            return new ResponseModel { responseCode = 200, objectResponse = cajaChica, message = "Success" };
        //        else
        //            return new ResponseModel { responseCode = 404, objectResponse = new List<Cajachica>(), message = "No se encontraron categorías." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new ResponseModel { responseCode = 500, objectResponse = new List<Cajachica>(), message = ex.Message };
        //    }
        //}

        //public async Task<ResponseModel> GetTotales(DateTime fechaInicio)
        //{
        //    try
        //    {
        //        var cajaChica = await db.Cajachicas.AsNoTracking().Where(asd => asd.IdCorte == null || asd.IdCorte == 0).ToListAsync();
        //        decimal entradas = 0;
        //        decimal salidas = 0;
        //        var totales = new CajaChicaModel();
        //        foreach (var item in cajaChica)
        //        {
        //            if (item.Tipo == "Entrada")
        //                entradas = entradas + item.Cantidad.Value;
        //            else
        //                salidas = salidas + item.Cantidad.Value;
        //        }

        //        totales.TotalEntradas = entradas;
        //        totales.TotalSalidas = salidas;

        //        if (cajaChica.Count() >= 1)
        //            return new ResponseModel { responseCode = 200, objectResponse = totales, message = "Success" };
        //        else
        //            return new ResponseModel { responseCode = 404, objectResponse = totales, message = "No se encontraron categorías." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new ResponseModel { responseCode = 500, objectResponse = new Cajachica(), message = ex.Message };
        //    }
        //}

        //public async Task<ResponseModel> GetById(int id)
        //{
        //    try
        //    {
        //        var cajaChica = await db.Cajachicas.AsNoTracking().Where(e => e.Id == id).ToListAsync();

        //        if (cajaChica.Count > 0)
        //            return new ResponseModel { responseCode = 200, objectResponse = cajaChica.First(), message = "Success" };
        //        else
        //            return new ResponseModel { responseCode = 404, objectResponse = new Cajachica(), message = "No se encontraron categorías." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new ResponseModel { responseCode = 500, objectResponse = new Cajachica(), message = ex.Message };
        //    }
        //}

        //public async Task<ResponseModel> Create(Cajachica regitro)
        //{
        //    try
        //    {
        //        db.Cajachicas.Add(regitro);

        //        var result = await db.SaveChangesAsync();
        //        if (result > 0)
        //            return new ResponseModel { responseCode = 200, objectResponse = result, message = "Movimiento registrado exitosamente." };
        //        else
        //            return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El usuario no existe." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
        //    }
        //}

        //public async Task<ResponseModel> Delete(int id)
        //{
        //    try
        //    {
        //        var regitro = db.Cajachicas.Where(u => u.Id == id).First<Cajachica>();
        //        db.Cajachicas.Remove(regitro);

        //        var result = await db.SaveChangesAsync();
        //        if (result > 0)
        //            return new ResponseModel { responseCode = 200, objectResponse = result, message = "Movimiento eliminado exitosamente" };
        //        else
        //            return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La categoría no pudo ser eliminada." };
        //    }
        //    catch (SqlException ex)
        //    {
        //        return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
        //    }
        //}

        //#endregion
    }
}