﻿using AutoMapper;
using CMSDataLayer.Models;
using DBManager.Models;
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
            CreateMap<LedColorsSeq, LEDRequestParams>().ForMember(dest => dest.SeqName, src => src.MapFrom(s => s.Data.Name.Iv))
                                                        .ForMember(dest => dest.Timer, src => src.MapFrom(s => (s.Data.TimerSeq.Iv).Select(t => Convert.ToInt32(t.TimerForEachColor)).ToList()))
                                                        .ForMember(dest => dest.Cycles, src => src.MapFrom(s => s.Data.Cycles.Iv))
                                                        .ForMember(dest => dest.Pattern, src => src.MapFrom(
                                                            s => s.Data.ColorSeq.Iv.Select(x => Convert.ToInt32(x.Colors.Replace("#", "0x"), 16)).ToList()));
            CreateMap<CollectionPoint, CPRequestParams>().ForMember(dest => dest.PointName, src => src.MapFrom(s => s.Data.PointName.Iv))
                .ForMember(dest => dest.CpIp, src => src.MapFrom(s => s.Data.CpIp.Iv))
                .ForMember(dest => dest.TriggerAnimation, src => src.MapFrom(s => s.Data.TriggerLedColorsSeq))
                .ForMember(dest => dest.SleepAnimation, src => src.MapFrom(s => s.Data.SleepLedColorsSeq))
                /*                .ForMember(dest => dest.PointName, src => src.MapFrom(s => s.Data.PointName.Iv))*/
                ;
            CreateMap<CPLampIncomingRequest, CPLampData>().ForMember(dest => dest.CPId, src => src.MapFrom(s => s.Id));
        }
    }
}
