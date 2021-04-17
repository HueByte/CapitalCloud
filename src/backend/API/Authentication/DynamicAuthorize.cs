using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Authentication
{
    // useless for now
    public class DynamicAuthorizeAttribute : TypeFilterAttribute
    {
        public DynamicAuthorizeAttribute() : base(typeof(DynamicAuthorizeFilter))
        {
            Arguments = new object[] { };
        }
    }

    public class DynamicAuthorizeFilter : IAuthorizationFilter
    {
        public DynamicAuthorizeFilter() { }

        // verifies roles from query
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isVerified = false;
            // check if query has any params
            if (context.HttpContext.Request.QueryString.HasValue)
            {
                // Get roles from query
                var @params = context.HttpContext.Request.Query;

                // Get roles from JWT
                var JWTRoles = context.HttpContext.User.Claims.Where(entity => entity.Type == ClaimTypes.Role);

                // Check if roles from query exists in JWT 
                foreach (var item in @params)
                {
                    isVerified = JWTRoles.Any(role => role.Value == item.Value);
                    if (isVerified == false) break;
                }
            }

            // if user hasn't got roles from query return forbidden
            if (!isVerified) context.Result = new ForbidResult();
        }
    }
}