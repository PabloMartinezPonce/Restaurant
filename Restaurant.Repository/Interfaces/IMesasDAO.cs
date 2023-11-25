using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IMesasDAO
    {
        Task<ResponseModel> Get();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> Create(Mesa registro);
        Task<ResponseModel> Update(Mesa registroView);
        Task<ResponseModel> Delete(int id);
        Task<ResponseModel> StatusOcupada(Mesa registroView);
    }
}