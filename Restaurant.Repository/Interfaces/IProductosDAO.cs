using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IProductosDAO
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> Get();
        Task<ResponseModel> GetAsComplement();
        Task<ResponseModel> GetByFilter(int id, string filter);
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> GetByName(string nombre);
        Task<ResponseModel> Create(Producto registro);
        Task<ResponseModel> AddIngredient(RelProductoRecetum registro);
        Task<ResponseModel> DeleteIngredient(int id);
        Task<ResponseModel> Update(Producto registroView);
        Task<ResponseModel> ControlStock(int idProducto, int unidadesVendidas);
        Task<ResponseModel> SetActivity(bool estatus, int Id);
        Task<ResponseModel> Delete(int id);
    }
}