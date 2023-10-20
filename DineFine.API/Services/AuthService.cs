using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using DineFine.Util;
using Microsoft.AspNetCore.Identity;

namespace DineFine.API.Services;

public class AuthService
{
    private readonly MssqlContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly EmailService _emailService;
    private readonly UserRoleService _userRoleService;

    public AuthService(MssqlContext context, UserManager<User> userManager, SignInManager<User> signInManager, 
        TokenService tokenService, EmailService emailManager, IConfiguration configuration, UserRoleService userRoleService)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailManager;
        _configuration = configuration;
        _userRoleService = userRoleService;
    }

    public async Task<IdentityResult> SignUpAsync(RegisterModel signUpModel)
    {
        var user = new User
        {
            UserName = signUpModel.FirstName + signUpModel.LastName,
            Email = signUpModel.Email,
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName
        };
        
        var password = signUpModel.Password;
        
        var result = await _userManager.CreateAsync(user, password);
        await _context.SaveChangesAsync();

        return result;
    }
    
    public async Task<IdentityResult> SignUpWithTenantAsync(RegisterModel signUpModel, int roleId, int tenantId)
    {
        var user = new User
        {
            UserName = signUpModel.FirstName + signUpModel.LastName,
            Email = signUpModel.Email,
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName,
            TenantId = tenantId
        };
        
        var roleResult = await _userRoleService.AddUserToRoleAsync(user.Id, roleId);
        
        if(!roleResult.Succeeded)
            throw DynamicExceptions.OperationalException("Error adding user to role");
        
        var password = signUpModel.Password;
        
        var result = await _userManager.CreateAsync(user, password);
        await _context.SaveChangesAsync();

        return result;
    }
    
    public async Task<TokenModel> LoginAsync(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);

        if (user is null) throw DynamicExceptions.NotFoundException("User not found");
        
        var password = loginModel.Password;
        var rememberMe = loginModel.RememberMe;
        
        var signInResult = await _signInManager.PasswordSignInAsync(user, password, rememberMe, true);
        
        if (!signInResult.Succeeded) throw DynamicExceptions.OperationalException("Invalid credentials");

        var result = await _tokenService.CreateAccessTokenAsync(user);
        
        return result;
    }
    
    public async Task LogoutAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null) throw DynamicExceptions.NotFoundException("User not found");
        
        await _tokenService.RevokeRefreshTokenAsync(user.Id);
        await _signInManager.SignOutAsync();
    }

    public async Task<TokenModel> RefreshAccessTokenAsync(string refreshToken)
    {
        if (refreshToken == null) throw DynamicExceptions.NullException("Refresh token is null");
        
        return await _tokenService.CreateAccessTokenByRefreshTokenAsync(refreshToken);
    }
    
    public async Task<string> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user is null) throw DynamicExceptions.NotFoundException("User not found");
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var appUrl = _configuration["profiles:https:applicationUrl"];
        var callbackUrl = Helpers.GenerateResetPasswordLink(appUrl!, email, token);

        Console.WriteLine(callbackUrl);
        
        await _emailService.SendResetPasswordEmail(callbackUrl, email);
        
        return "Reset password url sent to your email";
    }

    public async Task<IdentityResult> ResetPasswordAsync(int userId, string token, ResetPasswordModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null) throw DynamicExceptions.NotFoundException("User not found");
        
        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
        
        if (!result.Succeeded) throw DynamicExceptions.OperationalException("Password can not be reset");
        
        return result;
    }
}