using AutoMapper;
using CMSDataLayer.Models;
using FLDCVisitManagerBackend.Models;

namespace FLDCVisitManagerBackend.Helpers
{
    public class AutoMappingModels : Profile
    {
        public AutoMappingModels()
        {
            CreateMap<AppOptions, AppOptionsConfiguration>().ReverseMap();
        }
    }
}
