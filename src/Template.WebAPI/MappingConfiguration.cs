using System;
using AutoMapper;

namespace Template.WebAPI
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    class MappingConfiguration
    {
        public static IMapper Create()
        {
            Mapper.Initialize(cfg => {
                //cfg.CreateMap<Src, Dst>();
            });

            return Mapper.Instance;
        }
    }
}
