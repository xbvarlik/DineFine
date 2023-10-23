using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.Accessor.Mappings;
using DineFine.API.Constants;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class TableSessionService : BaseCosmosService<TableSession, TableSessionViewModel, TableSessionCreateModel, 
    TableSessionUpdateModel, TableSessionQueryFilterModel>
{
    public TableSessionService(CosmosContext context) : base(context)
    {
    }
    
    public IList<TableSessionViewModel> GetTableSessionsByDate(DateTime startDate, DateTime? endDate = null)
    {
        var sessions = GetEntityDbSetWithPartitionKey("RestaurantId");

        if (endDate == null)
            return sessions.Where(x => x.StartedAt.Date == startDate.Date)
                .AsEnumerable().ToTableSessionViewModelList().ToList();

        return sessions.Where(x => x.StartedAt.Date >= startDate.Date && x.StartedAt.Date <= endDate.Value.Date)
            .AsEnumerable().ToTableSessionViewModelList().ToList();
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