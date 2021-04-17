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
                var query = context.HttpContext.Request.QueryString.ToString();
                var roles = HttpUtility.ParseQueryString(query).ToString();

                Console.WriteLine(roles);
            }
            var hasClaim = context.HttpContext.User.Claims
                .Where(entity => entity.Type == _claim.Type && entity.Value == _claim.Value).ToArray();
            
            if(hasClaim.Length == 0) context.Result = new ForbidResult();
        }
    }
}