using System.Text.Json;

using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Infrastructure.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Syncfusion.Blazor;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            return Ok(_customerService.GetCustomer());
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<DataResultDto<CustomerGridDTM>>> GetCustomersAdaptor([FromBody] string payload)
        {
            var dm = JsonSerializer.Deserialize<DataManagerRequest>(payload);

            var result = (await _customerService.GetCustomer().ToDataResult(dm))
                .ToDataResultDto<CustomerGridDTM>();

            return Ok(result);
        }
        [HttpGet]
        public ActionResult<BaseResultDto<List<BranchDto>>> GetBranches()
        {
            var res = Enum.GetValues(typeof(BranchEnum.Branch))
                  .Cast<BranchEnum.Branch>()
                  .Select(c => new BranchDto()
                  {
                      Branch = c,
                      BranchName = (c as Enum).GetDisplayName()
                  }).ToList();

            return Ok(BaseResultDto<List<BranchDto>>.Success(res));
        }


        [HttpGet]
        public async Task<ActionResult<BaseResultDto<CustomerDetailDTM>>> GetCustomerDetail(long customerId)
        {
            if (customerId == 0)
            {
                return NotFound();
            }

            var res = await _customerService.GetCustomerDetail(customerId);

            return Ok(BaseResultDto<CustomerDetailDTM>.Success(res));
        }
    }
}
