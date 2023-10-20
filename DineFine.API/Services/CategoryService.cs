using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class CategoryService : BaseService<int, Category, CategoryViewModel, CategoryCreateModel, CategoryUpdateModel, 
    BaseQueryFilterModel, MssqlContext>
{
    public CategoryService(MssqlContext context) : base(context)
    {
    }

    protected override Task<IEnumerable<CategoryViewModel>> OnAfterGetAllAsync(IEnumerable<Category> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToCategoryViewModelList());
    }

    protected override CategoryViewModel? OnAfterGet(Category? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<Category> OnBeforeCreateAsync(CategoryCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<CategoryViewModel> OnAfterCreateAsync(Category entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<Category> OnBeforeUpdateAsync(Category entity, CategoryUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<CategoryViewModel> OnAfterUpdateAsync(Category entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}