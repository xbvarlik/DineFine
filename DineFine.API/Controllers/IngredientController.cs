using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

public class IngredientController : BaseController<int, Ingredient, IngredientViewModel, IngredientCreateModel, 
    IngredientUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public IngredientController(IngredientService service) : base(service)
    {
    }
    
    [SpecificAccess("RestaurantOwner, KitchenPersonnel")]
    public override Task<IActionResult> GetAllAsync(BaseQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        return base.GetAllAsync(queryFilter, cancellationToken);
    }

    [SpecificAccess("RestaurantOwner, KitchenPersonnel")]
    public override Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    [SpecificAccess("RestaurantOwner, KitchenPersonnel")]
    public override Task<IActionResult> CreateAsync(IngredientCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(createModel, cancellationToken);
    }
}