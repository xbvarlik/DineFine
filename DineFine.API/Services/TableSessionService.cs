using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.Accessor.Mappings;
using DineFine.API.Constants;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class TableSessionService : BaseCosmosService<TableSession, TableSessionViewModel, TableSessionCreateModel, 
    TableSessionUpdateModel, BaseCosmosQueryFilterModel>
{
    public TableSessionService(CosmosContext context) : base(context)
    {
    }

    protected override Task<IEnumerable<TableSessionViewModel>> OnAfterGetAllAsync(IEnumerable<TableSession> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToTableSessionViewModelList());
    }

    protected override TableSessionViewModel? OnAfterGet(TableSession? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<TableSession> OnBeforeCreateAsync(TableSessionCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<TableSessionViewModel> OnAfterCreateAsync(TableSession entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<TableSession> OnBeforeUpdateAsync(TableSession entity, TableSessionUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<TableSessionViewModel> OnAfterUpdateAsync(TableSession entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}