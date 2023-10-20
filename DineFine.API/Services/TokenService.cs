﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using DineFine.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DineFine.API.Services;

public class TokenService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly JwtBearerTokenSettings _jwtBearerTokenSettings;
    private readonly MssqlContext _appDbContext;
    private readonly DbSet<AppRefreshToken> _dbSet;

    public TokenService(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<JwtBearerTokenSettings> jwtBearerTokenSettings, MssqlContext appDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = appDbContext;
        _jwtBearerTokenSettings = jwtBearerTokenSettings.Value;
        _dbSet = _appDbContext.AppRefreshTokens;
    }

    private static SecurityKey GetSymmetricSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
    
    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User User, string audience)
    {
        var userClaims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, User.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, User.Email!),
            new (ClaimTypes.Name, User.UserName!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Aud, audience)
        };

        var userRoles = await _userManager.GetRolesAsync(User);
        
        foreach (var userRole in userRoles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, userRole));
            
            var roleNames = await _roleManager.FindByNameAsync(userRole);
            if (roleNames == null) continue;
            var roleClaims = await _roleManager.GetClaimsAsync(roleNames);

            userClaims.AddRange(roleClaims.Select(claim => new Claim(claim.Type, claim.Value)));
        }

        return userClaims;
    }
    
    private static string CreateRefreshToken()
    {
        var numberBytes = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberBytes);

        return Convert.ToBase64String(numberBytes);
    }
    
    private async Task<TokenModel> CreateToken(User User)
    {
        var accessTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(60);
        var refreshTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(360);
        var securityKey = GetSymmetricSecurityKey(_jwtBearerTokenSettings.SecurityKey);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtBearerTokenSettings.Issuer,
            expires: accessTokenExpirationDateTime,
            notBefore: DateTime.UtcNow,
            claims: await GetUserClaimsAsync(User, _jwtBearerTokenSettings.Audiences[0]),
            signingCredentials: signingCredentials
        );
        var jwtHandler = new JwtSecurityTokenHandler();
        var token = jwtHandler.WriteToken(jwtSecurityToken);
        
        return new TokenModel
        {
            UserId = User.Id,
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpirationDateTime,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpirationDateTime
        };
    }
    
    public async Task<TokenModel> CreateAccessTokenAsync(User User)
    {
        var accessTokenModel = await CreateToken(User);

        var appRefreshToken = await _dbSet.SingleOrDefaultAsync(x => x.UserId == User.Id);

        if (appRefreshToken == null)
        {
            await _dbSet.AddAsync(new AppRefreshToken
            {
                UserId = User.Id,
                RefreshToken = accessTokenModel.RefreshToken,
                RefreshTokenExpiration = accessTokenModel.RefreshTokenExpiration
            });
        }
        else
        {
            appRefreshToken.RefreshToken = accessTokenModel.RefreshToken;
            appRefreshToken.RefreshTokenExpiration =
                accessTokenModel.RefreshTokenExpiration;
        }

        await _appDbContext.SaveChangesAsync();
        return accessTokenModel;
    }
    
    public async Task<TokenModel> CreateAccessTokenByRefreshTokenAsync(string refreshToken)
    {
        var dbRefreshToken = await _dbSet.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (dbRefreshToken == null) 
            throw DynamicExceptions.NullException("Refresh token is null");

        var user = await _userManager.FindByIdAsync(dbRefreshToken.UserId.ToString());

        if (user == null) 
            throw DynamicExceptions.NotFoundException("User not found");

        var tokenModel = await CreateToken(user);
        dbRefreshToken.RefreshToken = tokenModel.RefreshToken;
        dbRefreshToken.RefreshTokenExpiration = tokenModel.RefreshTokenExpiration;

        await _appDbContext.SaveChangesAsync();
        return tokenModel;
    }
    
    public async Task RevokeRefreshTokenAsync(int UserId)
    {
        var dbRefreshToken = await _dbSet.SingleOrDefaultAsync(rt => rt.UserId == UserId);

        if (dbRefreshToken == null) 
            throw DynamicExceptions.NullException("Refresh token is null");

        _dbSet.Remove(dbRefreshToken);
        await _appDbContext.SaveChangesAsync();

            
    }
}