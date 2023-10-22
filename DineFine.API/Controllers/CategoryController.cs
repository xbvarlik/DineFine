using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

public class CategoryController : BaseController<int, Category, CategoryViewModel, CategoryCreateModel, CategoryUpdateModel, 
    BaseQueryFilterModel, MssqlContext>
{
    public CategoryController(CategoryService service) : base(service)
    {
    }

    [SpecificAccess("RestaurantOwner")]
    public override Task<IActionResult> GetAllAsync(BaseQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        return base.GetAllAsync(queryFilter, cancellationToken);
    }

    [SpecificAccess("RestaurantOwner")]
    public override Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }
}