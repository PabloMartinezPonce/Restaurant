using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface ICortesDAO
    {
        Task<ResponseModel> GetAllCortes();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> ClearSales(int idCorte);
        Task<ResponseModel> ClearAccount(int idCorte);
        Task<ResponseModel> ClearCajaChica(int idCorte);
        Task<ResponseModel> FinishSales(Corte registroView);
        Task<ResponseModel> Create(Corte registro);
        Task<ResponseModel> Delete(int id);
    }

}
