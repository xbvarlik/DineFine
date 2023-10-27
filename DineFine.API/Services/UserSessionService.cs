using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class UserSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserRoleService _userRoleService;
    private readonly TokenService _tokenService;
    private readonly CosmosContext _context;

    public UserSessionService(IHttpContextAccessor httpContextAccessor, TokenService tokenService, UserRoleService userRoleService, CosmosContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = tokenService;
        _userRoleService = userRoleService;
        _context = context;
    }

    private async Task<UserSession<TokenModel>?> GetUserSessionAsync(string userId)
    {
        return await _context.UserSessions.WithPartitionKey(userId).FirstOrDefaultAsync();
    }

    public async Task<UserSession<TokenModel>?> GetUserSessionByTokenAsync(string accessToken)
    {
        var response = await _context.UserSessions.FirstOrDefaultAsync(session => 
            session.LoginInfo!.AccessToken == accessToken);
        
        if(response == null) throw new DineFineNotFoundException();
        
        return response;
    }
    
    
    public async Task<TokenModel> CreateUserSessionAsync(User user)
    {
        var userRoles = await _userRoleService.GetUserRolesAsync(user.Id);
        
        if(userRoles is null) throw new DineFineNotFoundException();
        
        var sessionCreateModel = new UserSessionCreateModel
        {
            User = user,
            UserRoles = userRoles,
            TokenModel = await _tokenService.CreateAccessTokenAsync(user),
            Agent = _httpContextAccessor.HttpContext!.Request.Headers["User-Agent"].ToString()
        };
        
        var session = sessionCreateModel.ToUserSession();

        await UpsertUserUserSessionAsync(session);
        return session.LoginInfo!;
    }

    private async Task UpsertUserUserSessionAsync(UserSession<TokenModel> session)
    {
        var currentSession = await GetUserSessionAsync(session.UserId);

        if (currentSession is null)
            await _context.UserSessions.AddAsync(session);
        else
            _context.Entry(currentSession).CurrentValues.SetValues(session.ToUpdatedSession());

        await _context.SaveChangesAsync();
    }
}