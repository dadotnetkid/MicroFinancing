using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.WebAssembly.Common;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MicroFinancing.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LendingController : ControllerBase
{
    private readonly ICurrentUser _currentUser;
    private readonly ILendingService _lendingService;

    public LendingController(ILendingService lendingService,
        ICurrentUser currentUser)
    {
        _lendingService = lendingService;
        _currentUser = currentUser;
    }

    [HttpPost]
    public async Task<ActionResult<DataResultDto<LendingSummaryGridDTM>>> GetSummary([FromBody] string dm)
    {
        var dataManager = JsonSerializer.Deserialize<DataManagerRequest>(dm);

        if (!_currentUser.IsAuthorized(HttpContext.User, ClaimsConstant.Customer.ViewAllCustomer))
        {
            var query = (await _lendingService.GetSummary(dataManager, HttpContext.User.GetUserId()))
                .ToDataResultDto<LendingSummaryGridDTM>();

            return query;
        }

        var res = (await _lendingService.GetSummary(dataManager, null))
            .ToDataResultDto<LendingSummaryGridDTM>();

        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<DataResultDto<LendingForApprovalGridDTM>>> GetLendingNotApproved([FromBody] string item)
    {
        var dm = JsonConvert.DeserializeObject<DataManagerRequest>(item);

        var query = (await _lendingService.GetLendingNotApproved())
            .ToDataResultDto<LendingGridDTM>();

        return Ok(query);
    }

}