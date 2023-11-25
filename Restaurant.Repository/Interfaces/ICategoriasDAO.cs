using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface ICategoriasDAO
    {
        Task<ResponseModel> GetAllCategorias();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> Create(Categoria registro);
        Task<ResponseModel> Update(Categoria registroView);
        Task<ResponseModel> Delete(int id);
    }

}