using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("Waiter")]
public class TableSessionController : BaseCosmosController<TableSession, TableSessionViewModel, TableSessionCreateModel, 
    TableSessionUpdateModel, BaseQueryFilterModel>
{
    protected TableSessionController(TableSessionService service) : base(service)
    {
    }
}