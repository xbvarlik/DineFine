using DineFine.Accessor.DataAccessors.Cosmos;
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

    private async Task<UserSession<TokenModel>?> GetUserSessionAsync(int userId)
    {
        return await _context.UserSessions.FirstOrDefaultAsync(session => session.UserId == userId);
    }

    public async Task<UserSession<TokenModel>?> GetUserSessionByTokenAsync(string accessToken)
    {
        var response = await _context.UserSessions.FirstOrDefaultAsync(session => 
            session.LoginInfo.AccessToken == accessToken);
        return response;
    }
    
    
    public async Task<TokenModel> CreateUserSessionAsync(User user)
    {
        var userRoles = await _userRoleService.GetUserRolesAsync(user.Id);
        
        if(userRoles is null) throw new DineFineNotFoundException();
        
        var login = await _tokenService.CreateAccessTokenAsync(user);
        
        var session = CreteUserSessionEntity(user, userRoles, login);

        await UpsertUserUserSessionAsync(session);
        return login;
    }

    private async Task UpsertUserUserSessionAsync(UserSession<TokenModel> session)
    {
        var currentSession = await GetUserSessionAsync(session.UserId);
        
        if (currentSession is null)
            await _context.UserSessions.AddAsync(session);
        else
            _context.Entry(currentSession).CurrentValues.SetValues(session);

        await _context.SaveChangesAsync();
    }

    private UserSession<TokenModel> CreteUserSessionEntity(User user, IList<string> userRoles, TokenModel login)
    {
        return new UserSession<TokenModel>
        {
            UserId = user.Id,
            Agent = _httpContextAccessor.HttpContext!.Request.Headers["User-Agent"].ToString(),
            Email = user.Email!,
            UserRoles = userRoles,
            LoginInfo = login,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}