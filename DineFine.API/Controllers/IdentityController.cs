using DineFine.Accessor.Mappings;
using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[SpecificAccess("SuperAdmin")]
public class IdentityController : ControllerBase
{
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly UserRoleService _userRoleService;

    public IdentityController(UserService userService, RoleService roleService, UserRoleService userRoleService)
    {
        _userService = userService;
        _roleService = roleService;
        _userRoleService = userRoleService;
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetUsersAsync();
        var result = users.ToViewModelList();
        return ApiResult.CreateActionResult(ServiceResult<IEnumerable<UserViewModel>>.Success(200, result));
    }
    
    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _roleService.GetRolesAsync();
        var result = roles.AsEnumerable().ToRoleViewModelList();
        return ApiResult.CreateActionResult(ServiceResult<IEnumerable<RoleViewModel>>.Success(200, result));
    }
}