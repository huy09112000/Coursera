﻿using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared.ConfigMap
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            this.CreateMap<Question, QuestionsGivenDTO>();
        }
    }
}