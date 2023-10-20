using System.Security.Authentication;
using System.Security.Claims;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;
    private readonly UserSessionService _sessionService;
    private readonly UserManager<User> _userManager;

    public AuthController(AuthService service, UserSessionService sessionService, UserManager<User> userManager)
    {
        _service = service;
        _sessionService = sessionService;
        _userManager = userManager;
    }
    
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<IActionResult> SignUpAsync(RegisterModel signUpModel)
    {
        var response = await _service.SignUpAsync(signUpModel);
        
        if(response.Succeeded)
            return ApiResult.CreateActionResult(ServiceResult<IdentityResult>.Success(200, response));

        var errorList = response.Errors.Select(error => error.Description).ToList();

        return ApiResult.CreateActionResult(ServiceResult<IdentityResult>.Fail(400, errorList));
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> SignInAsync(LoginModel loginModel)
    {
        var response = await _service.LoginAsync(loginModel);
        
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if(user == null) throw DynamicExceptions.NotFoundException("User not found");
        
        await CreateUserSessionAsync(user);
        
        return ApiResult.CreateActionResult(ServiceResult<TokenModel>.Success(200, response));
    }
    
    [HttpPost("signup-with-tenant/{tenantId}/{roleId}")]
    public async Task<IActionResult> SignUpWithTenantIdAsync(RegisterModel signUpModel, [FromRoute] string tenantId, [FromRoute] string roleId)
    {
        var response = await _service.SignUpWithTenantAsync(signUpModel, int.Parse(roleId), int.Parse(tenantId));
        
        if(response.Succeeded)
            return ApiResult.CreateActionResult(ServiceResult<IdentityResult>.Success(200, response));

        var errorList = response.Errors.Select(error => error.Description).ToList();

        return ApiResult.CreateActionResult(ServiceResult<IdentityResult>.Fail(400, errorList));
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        var userName = HttpContext.User.Identity?.Name;

        if (userName == null) throw DynamicExceptions.NotFoundException("User not found");
        
        await _service.LogoutAsync(userName);
        return ApiResult.CreateActionResult(ServiceResult<NoContent>.Success(200));
    }
    
    [AllowAnonymous]
    [HttpGet("refresh-token")]
    public async Task<IActionResult> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        var result = await _service.RefreshAccessTokenAsync(refreshToken);

        return ApiResult.CreateActionResult(ServiceResult<TokenModel>.Success(200, result));
    }
    
    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(string email)
    {
        var result = await _service.ForgotPasswordAsync(email);

        return ApiResult.CreateActionResult(ServiceResult<string>.Success(200, result));
    }
    
    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromQuery] int userId, [FromQuery] string token, [FromBody] ResetPasswordModel resetPasswordModel)
    {
        var result = await _service.ResetPasswordAsync(userId, token, resetPasswordModel);

        return ApiResult.CreateActionResult(ServiceResult<IdentityResult>.Success(200 , result));
    }
    
    private async Task<TokenModel> CreateUserSessionAsync(User applicationUser)
    {
        if (applicationUser.Email is null)
            throw new InvalidCredentialException("Email cannot be empty");

        var claims = new List<Claim> { new(ClaimTypes.Email, applicationUser.Email) };

        var appIdentity = new ClaimsIdentity(claims);
        HttpContext.User.AddIdentity(appIdentity);

        return await _sessionService.CreateUserSessionAsync(applicationUser);
    }
}