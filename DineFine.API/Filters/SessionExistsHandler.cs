using DineFine.Accessor.SessionAccessors;
using DineFine.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DineFine.API.Filters;

public class SessionExistsHandler : AuthorizationHandler<SessionExistsRequirement>
{
    private readonly ISessionAccessor _sessionAccessor;
    private readonly UserSessionService _sessionService;
    
    public SessionExistsHandler(UserSessionService sessionService, ISessionAccessor sessionAccessor)
    {
        _sessionService = sessionService;
        _sessionAccessor = sessionAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SessionExistsRequirement requirement)
    {
        if (context.PendingRequirements.Any(x => x.GetType() == typeof(DenyAnonymousAuthorizationRequirement)))
        {
            context.Fail();
            return;
        }

        var token = _sessionAccessor.GetAccessToken();

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Fail();
            return;
        }

        var session = await _sessionAccessor.GetOrAddAsync(_ => _sessionService.GetUserSessionByTokenAsync(token));
        if (session != default)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}