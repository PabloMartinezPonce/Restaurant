using Microsoft.EntityFrameworkCore;
using Restaurante.Model;
using Restaurante.Data.DBModels;
using Restaurant.Repository.Interfaces;

namespace Restaurante.Data.DAO
{
    public class ComplementosDAO : IComplementosDAO
    {
        #region CRUD Entidad Complementos

        public async Task<ResponseModel> GetAllActiveComplementos(int idTipo = 0)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = new List<Complemento>();
                    if (idTipo != 0)
                        categorias = await db.Complementos
                            .Include("IdTipoComplementoNavigation")
                            .AsNoTracking()
                            .Where(com => com.Activo == true && com.IdTipoComplemento == idTipo)
                            .ToListAsync();
                    else
                        categorias = await db.Complementos.Include("IdTipoComplementoNavigation").AsNoTracking().Where(com => com.Activo == true).ToListAsync();

                    if (categorias.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetAllComplementos()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Complementos.Include("IdTipoComplementoNavigation").AsNoTracking().ToListAsync();

                    if (categorias.Count() >= 1)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetById(int id)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var categorias = await db.Complementos.AsNoTracking().Where(e => e.Id == id).ToListAsync();

                    if (categorias.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = categorias.First(), message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new Complemento(), message = "No se encontraron complementos." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new Complemento(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetAllTipos()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var result = await db.Complementos.AsNoTracking().Include("IdTipoComplementoNavigation").ToListAsync();

                    if (result.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Complemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Complemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> GetTipoComplementos()
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    var result = await db.Tipocomplementos.AsNoTracking().ToListAsync();

                    if (result.Count > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Success" };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = new List<Tipocomplemento>(), message = "No se encontraron complementos." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = new List<Tipocomplemento>(), message = ex.Message };
            }
        }

        public async Task<ResponseModel> Create(Complemento regitro)
        {
            try
            {
                using (var db = new restauranteContext())
                {
                    regitro.Activo = true;
                    db.Complementos.Add(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Complemento guardado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se efectuaron cambios." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> Update(Complemento regitroView)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Complementos.Where(u => u.Id == regitroView.Id).First<Complemento>();
                    if (!string.IsNullOrEmpty(regitroView.Nombre)) regitro.Nombre = regitroView.Nombre;
                    if (!string.IsNullOrEmpty(regitroView.Descripcion)) regitro.Descripcion = regitroView.Descripcion;
                    if (regitroView.Precio != 0) regitro.Precio = regitroView.Precio;
                    if (regitroView.IdTipoComplemento != 0 || regitroView.IdTipoComplemento != null) regitro.IdTipoComplemento = regitroView.IdTipoComplemento;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Complemento guardado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "No se efectuaron cambios." };
                }
            }
            catch (Exception ex)
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
                    var regitro = db.Complementos.Where(u => u.Id == id).First<Complemento>();
                    db.Complementos.Remove(regitro);

                    var result = await db.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "Complemento eliminado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = 0, message = "El complemento no pudo ser eliminada." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        public async Task<ResponseModel> SetActivity(bool estatus, int Id)
        {
            try
            {
                using (var con = new restauranteContext())
                {
                    var regitro = con.Complementos.Where(u => u.Id == Id).First<Complemento>();
                    regitro.Activo = !estatus;

                    var result = await con.SaveChangesAsync();
                    if (result > 0)
                        return new ResponseModel { responseCode = 200, objectResponse = result, message = "El complemento fue actualizado exitosamente." };
                    else
                        return new ResponseModel { responseCode = 404, objectResponse = null, message = "El complemento no pudo ser actualizado." };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel { responseCode = 500, objectResponse = 0, message = ex.Message };
            }
        }

        #endregion
    }
}