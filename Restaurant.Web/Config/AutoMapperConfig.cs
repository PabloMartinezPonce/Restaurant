using AutoMapper;
using Restaurante.Data.DBModels;
using Restaurante.Model;

namespace Restaurant.Web.Config
{
    public class AutoMapperConfig : Profile
    {
        public void UserProfile()
        {
            CreateMap<Cajachica, CajachicaDTO>().ReverseMap();
        }
    }
}