using AutoMapper;
using project.Models;
using project.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Shared.ConfigMap
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserDTO>();
            this.CreateMap<UserDTO, User>();
            this.CreateMap<Point, PointAfterSubmitDTO>();
            this.CreateMap<PointAfterSubmitDTO, Point>();
        }
    }
}