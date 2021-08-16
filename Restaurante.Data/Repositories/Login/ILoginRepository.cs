using Restaurante.Model;
using System.Threading.Tasks;

namespace Restaurante.Data.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<ResponseModel> ValidateLogin(string usuario, string contrasena, string query);
        Task<ResponseModel> DisableAccount(string usuario, string query);
    }
}
