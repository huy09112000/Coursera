using project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project.Filter
{
    public class FilterAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly int[] allowedroles;
        public FilterAuthorizeAttribute(params int[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToInt32(httpContext.Session["id"]);
            if (userId != 0)
                using (EducationDBContext context = new EducationDBContext())
                {
                    var userRole = (from u in context.Users

                                    where u.Id == userId
                                    select new
                                    {
                                        u.Role
                                    }).FirstOrDefault();
                    foreach (var role in allowedroles)
                    {
                        if (role == userRole.Role) return true;
                    }
                }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "Index" }
               });
        }
    }
}