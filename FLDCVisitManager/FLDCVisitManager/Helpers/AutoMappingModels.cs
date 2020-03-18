using AutoMapper;
using CMSDataLayer.Models;
using FLDCVisitManagerBackend.Models;
using System;
using System.Linq;

namespace FLDCVisitManagerBackend.Helpers
{
    public class AutoMappingModels : Profile
    {
        public AutoMappingModels()
        {
            CreateMap<AppOptions, AppOptionsConfiguration>().ReverseMap();
            CreateMap<LedColorsSeq, LEDRequestParams>().ForMember(dest => dest.SeqName, src => src.MapFrom(s => s.Data.Name))
                                                        .ForMember(dest => dest.Timer, src => src.MapFrom(s => s.Data.TimerSeq.Iv))
                                                        .ForMember(dest => dest.Cycles, src => src.MapFrom(s => s.Data.Cycles.Iv))
                                                        .ForMember(dest => dest.Pattern, src => src.MapFrom(
                                                            s => s.Data.ColorSeq.Iv.Select(x => Convert.ToInt32(x.Colors.Replace("#", "0x"), 16)).ToList()));
        }
    }
}
