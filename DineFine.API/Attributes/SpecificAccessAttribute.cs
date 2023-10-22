using DineFine.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DineFine.API.Attributes;

public class SpecificAccessAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly List<string> _allowRoles;

    public SpecificAccessAttribute(string allowRoles)
    {
        _allowRoles = allowRoles.Split(",").Select(x => x.Trim()).ToList();
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (IsOpenRoute(context))
            return;
        
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated) context.Result = new UnauthorizedResult();
        
        var authorizedForRole = _allowRoles.Exists(role => user.IsInRole(role));
        
        var authorized = authorizedForRole || IsUserAuthorizedInOtherAttributes(context);
        
        if (!authorized) context.Result = new ForbidResult();
    }
    
    private bool IsUserAuthorizedInOtherAttributes(ActionContext context)
    {
        // Check other attributes
        var attributes = context.ActionDescriptor.EndpointMetadata.OfType<SpecificAccessAttribute>().ToList();

        return attributes.Exists(attr =>
            attr._allowRoles != this._allowRoles && 
            attr._allowRoles.Exists(x => context.HttpContext.User.IsInRole(x))
        );
    }

    private static bool IsOpenRoute(ActionContext context)
    {
        //Check if AllowAnonymous attribute is present
        var attributes = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().ToList();
        return attributes.Any();
    }
}