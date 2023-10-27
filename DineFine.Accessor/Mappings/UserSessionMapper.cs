using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class UserSessionMapper
{
    public static UserSession<TokenModel> ToUserSession(this UserSessionCreateModel model)
    {
        return new UserSession<TokenModel>
        {
            UserSessionId = Guid.NewGuid().ToString(),
            UserId = model.User.Id.ToString(),
            Agent = model.Agent,
            Email = model.User.Email!,
            TenantId = model.User.TenantId,
            UserRoles = model.UserRoles,
            LoginInfo = model.TokenModel,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static UserSessionUpdateModel ToUpdatedSession(this UserSession<TokenModel> session)
    {
        return new UserSessionUpdateModel
        {
            UserId = session.UserId,
            Agent = session.Agent,
            Email = session.Email!,
            UserRoles = session.UserRoles,
            TenantId = session.TenantId,
            LoginInfo = session.LoginInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}