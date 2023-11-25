using AutoMapper;
using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Repository.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Cajachica, CajachicaDTO>().ReverseMap();
        }
    }
}