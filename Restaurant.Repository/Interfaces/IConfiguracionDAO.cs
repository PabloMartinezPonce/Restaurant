using Restaurante.Model;
using Restaurante.Model.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface IConfiguracionDAO
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetById(int id);
        Task<ResponseModel> Update(ConfigModel config);
        Task<ResponseModel> ActiveDarkMode(bool active);
    }
}