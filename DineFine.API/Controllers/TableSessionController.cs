using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("Waiter")]
public class TableSessionController : BaseController<string, TableSession, TableSessionViewModel, TableSessionCreateModel, 
    TableSessionUpdateModel, BaseQueryFilterModel, CosmosContext>
{
    protected TableSessionController(TableSessionService service) : base(service)
    {
    }
}