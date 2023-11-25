using Microsoft.EntityFrameworkCore;
using Restaurant.Repository.Interfaces;
using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurante.Data.DAO
{
    public class MesasDAO : IMesasDAO
    {
        #region CRUD Entidad Mesas

        public async Task<ResponseModel> Get()
        {

            using (var db = new restauranteContext())
            {
                var mesas = await db.Mesas.AsNoTracking().Where(cr => cr.Ocupada == false).ToListAsync();

                if (mesas.Any())
                    return new ResponseModel { responseCode = 200, objectResponse = mesas, message = "Success" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = mesas, message = "No se encontraron mesas." };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            using (var db = new restauranteContext())
            {
                var mesa = await db.Mesas.FindAsync(id);

                if (mesa != null)
                    return new ResponseModel { responseCode = 200, objectResponse = mesa, message = "Success" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = mesa, message = "No se encontraron mesas." };
            }
        }

        public async Task<ResponseModel> Create(Mesa regitro)
        {
            using (var db = new restauranteContext())
            {
                db.Mesas.Add(regitro);
                var result = await db.SaveChangesAsync();

                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa guardada exitosamente." };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
            }
        }

        public async Task<ResponseModel> Update(Mesa regitroView)
        {
            using (var db = new restauranteContext())
            {
                var registro = await db.Mesas.FirstOrDefaultAsync(m => m.Id == regitroView.Id);
                if (registro == null) return new ResponseModel { responseCode = 404, objectResponse = null, message = "Mesa no encontrada." };
                if (!string.IsNullOrEmpty(regitroView.Descripcion)) registro.Descripcion = regitroView.Descripcion;

                var result = await db.SaveChangesAsync();
                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa guardada exitosamente." };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
            }
        }

        public async Task<ResponseModel> Delete(int id)
        {
            using (var db = new restauranteContext())
            {
                var registro = await db.Mesas.FindAsync(id);
                if (registro == null)
                    return new ResponseModel { responseCode = 404, objectResponse = null, message = "Mesa no encontrada." };

                db.Mesas.Remove(registro);
                var result = await db.SaveChangesAsync();

                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "Mesa eliminada exitosamente." };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser eliminada." };
            }
        }

        public async Task<ResponseModel> StatusOcupada(Mesa regitroView)
        {
            using (var db = new restauranteContext())
            {
                var registro = await db.Mesas.FirstOrDefaultAsync(m => m.Id == regitroView.Id);
                registro.Ocupada = regitroView.Ocupada;

                var result = await db.SaveChangesAsync();

                if (result > 0)
                    return new ResponseModel { responseCode = 200, objectResponse = result, message = "Éxito" };
                else
                    return new ResponseModel { responseCode = 404, objectResponse = 0, message = "La mesa no pudo ser guardada." };
            }
        }

        #endregion
    }
}