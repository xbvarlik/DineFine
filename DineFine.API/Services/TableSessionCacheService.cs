using DineFine.Accessor.HttpAccessors;
using DineFine.Accessor.SessionAccessors;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using DineFine.Util;

namespace DineFine.API.Services;

public class TableSessionCacheService
{
    private readonly TableSessionService _tableSessionService;
    private readonly ISessionAccessor _sessionAccessor;
    private readonly HttpAccessor _httpAccessor;

    public TableSessionCacheService(TableSessionService tableSessionService, ISessionAccessor sessionAccessor, HttpAccessor httpAccessor)
    {
        _tableSessionService = tableSessionService;
        _sessionAccessor = sessionAccessor;
        _httpAccessor = httpAccessor;
    }

    public void CreateTableSession(TableOfRestaurantViewModel table)
    {
        var accessKey = GenerateTableSessionAccessKey();
        _sessionAccessor.AddObjectToCache(accessKey, GenerateTableSession(table));
        _httpAccessor.SetHeader("TableSessionAccessKey", accessKey);
    }


    public void UpdateTableSession(TableSession tableSession)
    {
        var key = _sessionAccessor.GetTableSessionAccessKey();
        if (key == null) return;
        
        _sessionAccessor.UpdateObjectInCache(key, tableSession);
    }

    public async Task KillTableSessionAsync()
    {
        var key = _sessionAccessor.GetTableSessionAccessKey();
        if (key == null) return;
        
        var tableSession = await GetTableSessionAsync();
        if (tableSession != null)
            _tableSessionService.CreateTableSessionFromEntityAsync(tableSession);
        
        _sessionAccessor.RemoveObjectFromCache(key);
        
        _httpAccessor.SetHeader("TableSessionAccessKey", null);
    }

    public async Task<TableSession?> GetTableSessionAsync()
    {
        var key = _sessionAccessor.GetTableSessionAccessKey();
        if (key == null) return null;
        
        return await _sessionAccessor.GetAsync<TableSession>(key);
    }

    private static string GenerateTableSessionAccessKey()
        => Helpers.GenerateRandomSecret();
    
    private static TableSession GenerateTableSession(TableOfRestaurantViewModel tableOfRestaurant)
    {
        return new TableSession
        {
            TableSessionId = Guid.NewGuid().ToString(),
            RestaurantId = tableOfRestaurant.RestaurantId.ToString(),
            Orders = new List<OrderViewModel>(),
            StartedAt = DateTime.Now,
            EndedAt = DateTime.Now.AddDays(1),
            Restaurant = tableOfRestaurant.Restaurant!,
            TableOfRestaurant = tableOfRestaurant,
            TotalPrice = 0
        };
    }
}