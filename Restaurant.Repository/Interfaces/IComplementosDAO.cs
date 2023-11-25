using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IComplementosDAO
    {
        Task<ResponseModel> GetAllActiveComplementos(int idTipo = 0);
        Task<ResponseModel> GetAllComplementos();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> GetAllTipos();
        Task<ResponseModel> GetTipoComplementos();
        Task<ResponseModel> Create(Complemento registro);
        Task<ResponseModel> Update(Complemento registroView);
        Task<ResponseModel> Delete(int id);
        Task<ResponseModel> SetActivity(bool estatus, int Id);
    }

}