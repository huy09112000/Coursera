using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared.ConfigMap
{
    public class QuizzProfile:Profile
    {
        public QuizzProfile()
        {
            this.CreateMap<Quizz, QuizzDTO>();
            this.CreateMap<QuizzDTO, Quizz>();
        }
    }
}