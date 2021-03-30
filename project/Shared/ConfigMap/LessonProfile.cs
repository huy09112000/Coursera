using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared.ConfigMap
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            this.CreateMap<Lession, LessonDTO>();
            this.CreateMap<LessonDTO, Lession>();
        }
    }
}