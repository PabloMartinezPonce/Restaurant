using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IUsuariosDAO
    {
        Task<ResponseModel> GetUsers();
        Task<ResponseModel> GetUserById(int id);
        Task<ResponseModel> GetUserByUserName(string userName);
        Task<ResponseModel> GetUserByRol(int idRol);
        Task<ResponseModel> CreateUser(Usuario usuario);
        Task<ResponseModel> UpdateUser(Usuario usuario);
        Task<ResponseModel> ModifyStatus(string userName, bool estatus);
        Task<ResponseModel> UpdateImage(int id, string fileName);
        Task<ResponseModel> DeleteUser(int id);
    }

}