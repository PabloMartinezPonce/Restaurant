using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface ICuentasDAO
    {
        Task<ResponseModel> GetCuentaByIdEmpleado(int IdEmpleado, bool estaActiva);
        Task<ResponseModel> Create(Cuenta cuentas);
        Task<ResponseModel> Cancel(int idCuenta);
        Task<ResponseModel> CreateAssistant(Cuenta cuentas);
        Task<ResponseModel> AddProducto(RelCuentaProducto producto);
        Task<ResponseModel> DeleteProduct(int id);
        Task<ResponseModel> CreatePayment(Venta venta, int idMesa);
        Task<ResponseModel> StatusCuenta(int id);
        Task<ResponseModel> GetAll(bool estaActiva);
        Task<ResponseModel> GetById(int id);
        Task<bool> Exist(int id);
    }
}