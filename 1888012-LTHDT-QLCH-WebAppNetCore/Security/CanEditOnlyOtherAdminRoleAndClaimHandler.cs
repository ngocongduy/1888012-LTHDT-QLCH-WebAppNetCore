using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Security
{
    public class CanEditOnlyOtherAdminRoleAndClaimHandler :
        AuthorizationHandler<ManageAdminRoleAndClaimRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanEditOnlyOtherAdminRoleAndClaimHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRoleAndClaimRequirement requirement)
        {
            //Get context to be checked - which call the handler - cannot use in core 3.0
            //var authFilterContext = context.Resource as AuthorizationFilterContext;
            //if (authFilterContext == null)
            //{
            //    return Task.CompletedTask;
            //}
            //Get context to be checked - which call the handler
            var routeData = _httpContextAccessor.HttpContext.Request.Query["userId"];
            if (string.IsNullOrEmpty(routeData.FirstOrDefault()))
            {
                return Task.CompletedTask;
            }
            string loggedInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            //To edit a user roles/claims, our app will send a request with user being edited id to server
            //string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"] - cannot use in core 3.0
            string adminIdBeingEdited = routeData.FirstOrDefault();
            if (context.User.IsInRole("Admin") 
                && context.User.HasClaim(c => c.Type == "Edit Role" && c.Value == "Edit Role")
                && adminIdBeingEdited.ToUpper() != loggedInAdminId.ToUpper()) 
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
