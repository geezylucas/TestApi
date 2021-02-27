using AutoMapper;
using ConnectData.Model;

namespace ConnectData.DTOs
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
