using AutoMapper;
using project.Shared.ConfigMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared
{
    public static class AutoMap
    {
        public static IMapper Mapper { get; set; }

        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(
               config =>
               {
                   config.AddProfile<SubjectProfile>();
               });

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}