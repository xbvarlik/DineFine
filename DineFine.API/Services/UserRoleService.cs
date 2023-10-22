using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.DataObjects.Entities;
using DineFine.Exception;
using Microsoft.AspNetCore.Identity;

namespace DineFine.API.Services;

public class UserRoleService
{
    private readonly MssqlContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    
    public UserRoleService(MssqlContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IdentityResult> AddUserToRoleAsync(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (user == null) throw new DineFineNotFoundException();
        if (role == null) throw new DineFineNotFoundException();
        if (await IsUserInRoleAsync(user, role.Name!)) throw new DineFineOperationalException("User is already in role");
        
        var result = await _userManager.AddToRoleAsync(user, role.Name!);
        
        if (!result.Succeeded) throw new DineFineOperationalException("Error adding user to role");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IdentityResult> RemoveUserFromRoleAsync(int userId, int roleId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (user == null) throw new DineFineNotFoundException();
        if (role == null) throw new DineFineNotFoundException();
        if (!await IsUserInRoleAsync(user, role.Name!)) throw new DineFineOperationalException("User is not in role");
        
        var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);
        
        if (!result.Succeeded) throw new DineFineOperationalException("Error removing user from role");
        
        await _context.SaveChangesAsync();
        return result;
    }
    
    public async Task<IList<string>> GetUserRolesAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user == null) throw new DineFineNotFoundException();
        
        return await _userManager.GetRolesAsync(user);
    }
    
    public async Task<List<User>> GetRoleUsersAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (role == null) throw new DineFineNotFoundException();

        return (await _userManager.GetUsersInRoleAsync(role.Name!) as List<User>)!;
    }

    private async Task<bool> IsUserInRoleAsync(User user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }
}