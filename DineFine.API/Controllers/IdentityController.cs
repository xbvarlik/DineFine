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

    public IdentityController(UserService userService, RoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
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