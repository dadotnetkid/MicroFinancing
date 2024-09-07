using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerGridDTM>>> GetCustomers(string? customerName)
    {
        var userId = User.GetUserId();

        var res = _customerService.GetCustomerByCollector(userId);

        if (!string.IsNullOrEmpty(customerName))
        {
            res = res.Where(x => x.FullName.Contains(customerName));
        }
        return Ok(await res.ToListAsync());
    }
}
