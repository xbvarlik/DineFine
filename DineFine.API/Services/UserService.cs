﻿using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.DataObjects.Entities;
using DineFine.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class UserService
{
    private readonly MssqlContext _context;
    private readonly UserManager<User> _userManager;

    public UserService(MssqlContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
        
        if (user == null) throw DynamicExceptions.NotFoundException("User not found");

        return user;
    }
}