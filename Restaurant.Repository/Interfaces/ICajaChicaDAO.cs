using Restaurante.Model;

namespace Restaurant.Repository.Interfaces
{
    public interface ICajaChicaDAO
    {
        public Task<ResponseModel> GetAll(DateTime fechaInicio);
        public Task<ResponseModel> GetTotales(DateTime fechaInicio);
        public Task<ResponseModel> GetById(int id);
        public Task<ResponseModel> Create(CajachicaDTO regitro);
        public Task<ResponseModel> Delete(int id);
    }
}