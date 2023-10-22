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
public class BaseCosmosController<TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel> : ControllerBase
    where TEntity: class
    where TViewModel: BaseViewModel
    where TCreateModel: BaseCreateModel
    where TUpdateModel: BaseUpdateModel
    where TQueryFilterModel: BaseCosmosQueryFilterModel?
{   
    protected readonly BaseCosmosService<TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel> Service;

    protected BaseCosmosController(BaseCosmosService<TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel> service)
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
    public virtual async Task<IActionResult> GetByIdAsync([FromRoute]string id, [FromQuery]string? partitionKey, CancellationToken cancellationToken = default)
    {
        var result = await Service.GetByIdAsync(id, partitionKey, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(200, result));
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.CreateAsync(createModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(201, result));
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> UpdateAsync(string id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.UpdateAsync(id, updateModel, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204, result));
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await Service.DeleteAsync(id, cancellationToken);
        return ApiResult.CreateActionResult(ServiceResult<TViewModel>.Success(204));
    }
}