using AutoMapper;
using ConnectData.Model;

namespace BusinessLogic.DTOs
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();
        }
    }
}
