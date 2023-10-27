using DineFine.Accessor.SessionAccessors;
using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[SpecificAccess("SuperAdmin")]
public class BaseTenantController<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> : ControllerBase
    where TEntity: class
    where TViewModel: BaseViewModel
    where TCreateModel: BaseCreateModel
    where TUpdateModel: BaseUpdateModel
    where TQueryFilterModel: BaseQueryFilterModel?
    where TDbContext: DbContext
{
    private readonly BaseTenantService<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> _service;
    private readonly int _tenantId;
    
    protected BaseTenantController(BaseTenantService<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> service, ISessionAccessor sessionAccessor)
    {
        _service = service;
        _tenantId = sessionAccessor.AccessTenantId()!.Value;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync([FromQuery]TQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        var result = await _service.GetAllAsync(_tenantId, queryFilter, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<IEnumerable<TViewModel>>.Success(200, result));
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute]TId id, CancellationToken cancellationToken = default)
    {
        var result = await _service.GetByIdAsync(id, _tenantId, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(200, result));
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await _service.CreateAsync(createModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(201, result));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(TId id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await _service.UpdateAsync(id, updateModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204, result));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204));
    }
}
