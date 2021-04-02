using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<Course, CourseDTO>();
        }
    }
}