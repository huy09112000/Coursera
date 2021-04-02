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
                   config.AddProfile<LessonProfile>();
                   config.AddProfile<QuizzProfile>();
                   config.AddProfile<QuestionProfile>();
                   config.AddProfile<AnswerProfile>();
                   config.AddProfile<UserProfile>();
               });

            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}