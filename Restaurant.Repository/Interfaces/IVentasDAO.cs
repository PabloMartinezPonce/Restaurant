using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IVentasDAO
    {
        Task<ResponseModel> Get(DateTime fechaInicio, DateTime fechaFin, int? idCorte);
        Task<ResponseModel> GetForCorte(DateTime fechaInicio, DateTime fechaFin);
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> GetProductDetails(int id);
        Task<ResponseModel> Create(Venta registro);
        Task<ResponseModel> Delete(int id);
    }

}