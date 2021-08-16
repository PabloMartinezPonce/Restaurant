using Restaurante.Model;
using System.Threading.Tasks;

namespace Restaurante.Data.Repositories.DynamicQuery
{
    public interface IDynamicRepository
    {
        Task<ResponseModel> DynamicInsert(dynamic model, string query);
        Task<ResponseModel> DynamicDelete(int id, string query);
        Task<ResponseModel> DynamicSelect(int id, string queryAll, string queryById);
    }
}