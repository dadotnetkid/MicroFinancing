using System.Text.Json;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.WebAssembly.Common;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor;

namespace MicroFinancing.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;
    private readonly ClaimsValueModel _claimsValueModel;

    public PermissionController(IPermissionService permissionService, ClaimsValueModel claimsValueModel)
    {
        _permissionService = permissionService;
        _claimsValueModel = claimsValueModel;
    }

    [HttpPost]
    public async Task<ActionResult<DataResultDto<PermissionGridDTM>>> GetPermissions([FromBody] string item)
    {
        var dm = JsonSerializer.Deserialize<DataManagerRequest>(item);
        var result = (await _permissionService.GetPermissions(dm)).ToDataResultDto<PermissionGridDTM>();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BaseResultDto<bool>>> Create([FromBody] CreateUpdatePermissionDTM item)
    {
        await _permissionService.Create(item);
        return Ok(BaseResultDto<bool>.Success(true));
    }

    [HttpPut]
    public async Task<ActionResult<BaseResultDto<bool>>> Update([FromBody] CreateUpdatePermissionDTM item)
    {
        await _permissionService.Update(item);
        return Ok(BaseResultDto<bool>.Success(true));
    }

    [HttpGet]
    public ActionResult<BaseResultDto<List<ClaimsLookup>>> ClaimsValueModels()
    {
        return Ok(BaseResultDto<List<ClaimsLookup>>.Success(_claimsValueModel.ClaimsValueModels));
    }
}