using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared.ConfigMap
{
    public class AnswerProfile:Profile
    {
        public AnswerProfile()
        {
            this.CreateMap<Answer, AnswerDTO>();
            this.CreateMap<AnswerDTO, Answer>();
        }
    }
}