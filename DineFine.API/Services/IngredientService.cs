using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class IngredientService : BaseService<int, Ingredient, IngredientViewModel, IngredientCreateModel, 
    IngredientUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public IngredientService(MssqlContext context) : base(context)
    {
    }

    protected override Task<IEnumerable<IngredientViewModel>> OnAfterGetAllAsync(IEnumerable<Ingredient> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToIngredientViewModelList());
    }

    protected override IngredientViewModel? OnAfterGet(Ingredient? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<Ingredient> OnBeforeCreateAsync(IngredientCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<IngredientViewModel> OnAfterCreateAsync(Ingredient entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<Ingredient> OnBeforeUpdateAsync(Ingredient entity, IngredientUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<IngredientViewModel> OnAfterUpdateAsync(Ingredient entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}