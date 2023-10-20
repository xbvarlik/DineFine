using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class RoleService 
{
    private readonly MssqlContext _context;
    private readonly RoleManager<Role> _roleManager;

    public RoleService(MssqlContext context, RoleManager<Role> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }
    
    public async Task<List<Role>> GetRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }
    
    public async Task<Role> GetRoleByIdAsync(int id)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw DynamicExceptions.NotFoundException("Role not found");

        return role;
    }
    
    public async Task<RoleViewModel> CreateRoleAsync(RoleCreateModel model)
    {
        var role = model.ToEntity();
        var result = await _roleManager.CreateAsync(role);
        
        if (!result.Succeeded) throw DynamicExceptions.OperationalException("Error creating role");
        
        await _context.SaveChangesAsync();
        return role.ToViewModel();
    }

    public async Task UpdateRoleAsync(int id, RoleUpdateModel model)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw DynamicExceptions.NotFoundException("Role not found");
        
        role.ToUpdatedEntity(model);
        var result = await _roleManager.UpdateAsync(role);
        
        if (!result.Succeeded) throw DynamicExceptions.OperationalException("Error updating role");
        
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteRoleAsync(int id)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
        
        if (role == null) throw DynamicExceptions.NotFoundException("Role not found");
        
        var result = await _roleManager.DeleteAsync(role);
        
        if (!result.Succeeded) throw DynamicExceptions.OperationalException("Error deleting role");
        
        await _context.SaveChangesAsync();
    }
}