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
        public DynamicAuthorizeAttribute(string claimType, string claimValue) : base(typeof(DynamicAuthorizeFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class DynamicAuthorizeFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public DynamicAuthorizeFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //dynamic here
            if(context.HttpContext.Request.QueryString.HasValue)
            {
                // var zw = context.HttpContext.Request.QueryString;
                // var query = context.HttpContext.Request.QueryString.ToString();
                // var roles = HttpUtility.ParseQueryString(query);
                // var arr = roles.AllKeys.ToDictionary(x => x, x => roles[x]);
                // var z = arr.Values.ToArray();

                var query = context.HttpContext.Request.Query;
                // var x = query.ToDictionary(x => x, x => query[x]).Values.ToArray();\


                //TO FINISH
                var JWTRoles = context.HttpContext.User.Claims.Where(entity => entity.Type == ClaimTypes.Role);
            }
            var hasClaim = context.HttpContext.User.Claims
                .Where(entity => entity.Type == _claim.Type && entity.Value == _claim.Value).ToArray();
            
            if(hasClaim.Length == 0) context.Result = new ForbidResult();
        }
    }
}