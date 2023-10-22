using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[SpecificAccess("SuperAdmin")]
public class BaseController<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> : ControllerBase
    where TEntity: class
    where TViewModel: BaseViewModel
    where TCreateModel: BaseCreateModel
    where TUpdateModel: BaseUpdateModel
    where TQueryFilterModel: BaseQueryFilterModel?
    where TDbContext: DbContext
{   
    protected readonly BaseService<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> Service;

    protected BaseController(BaseService<TId, TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel, TDbContext> service)
    {
        Service = service;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAllAsync([FromQuery]TQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        var result = await Service.GetAllAsync(queryFilter, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<IEnumerable<TViewModel>>.Success(200, result));
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute]TId id, CancellationToken cancellationToken = default)
    {
        var result = await Service.GetByIdAsync(id, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(200, result));
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.CreateAsync(createModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(201, result));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(TId id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.UpdateAsync(id, updateModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204, result));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        await Service.DeleteAsync(id, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204));
    }
}
