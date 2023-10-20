using DineFine.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DineFine.Util;


/// <summary>
/// Custom authorize attribute to open specific routes to specific roles.
/// You can specify who can enter the controller at the top (class-level),
/// and who can enter the action at the bottom (action-level).
/// The roles at the class level have access to all actions in the controller.
/// The roles at the action level can only access that action.
/// So, if you want to open an action JUST FOR ONE ROLE and don't want the class level roles to access it,
/// you can use the default [Authorize] attribute.
/// </summary>

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public readonly List<string> _allowRoles;

    public CustomAuthorizeAttribute(string allowRoles)
    {
        _allowRoles = allowRoles.Split(",").Select(x => x.Trim()).ToList();
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity == null)
            throw DynamicExceptions.UnauthorizedException("User identity is null.");
        
        if (!user.Identity.IsAuthenticated) context.Result = new UnauthorizedResult();
        
        var authorizedForRole = _allowRoles.Exists(role => user.IsInRole(role));
        
        var authorized = authorizedForRole || IsUserAuthorizedInOtherAttributes(context);
        
        if (!authorized) context.Result = new ForbidResult();
    }
    
    private bool IsUserAuthorizedInOtherAttributes(AuthorizationFilterContext context)
    {
        // Check other attributes
        var attributes = context.ActionDescriptor.EndpointMetadata.OfType<CustomAuthorizeAttribute>().ToList();

        return attributes.Exists(attr =>
            attr._allowRoles != this._allowRoles && 
            attr._allowRoles.Exists(x => context.HttpContext.User.IsInRole(x))
        );
    }
}