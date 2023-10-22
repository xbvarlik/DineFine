using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel")]
public class MenuItemIngredientController : BaseController<int, MenuItemIngredient, MenuItemIngredientViewModel, 
    MenuItemIngredientCreateModel, MenuItemIngredientUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public MenuItemIngredientController(MenuItemIngredientService service) : base(service)
    {
    }
}