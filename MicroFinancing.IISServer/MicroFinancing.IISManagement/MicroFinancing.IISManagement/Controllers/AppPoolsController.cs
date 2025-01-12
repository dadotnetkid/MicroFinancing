using System.Security.Cryptography.X509Certificates;

using MicroFinancing.IISManagement.Client.Clients;
using MicroFinancing.IISManagement.Client.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Administration;

namespace MicroFinancing.IISManagement.Controllers;

[ApiController]
[Route($"api/{Endpoints.AppPools.ControllerName}")]
public class AppPoolsController : ControllerBase
{
    private readonly ServerManager _serverManager;

    public AppPoolsController()
    {
        _serverManager = new ServerManager();
    }

    [HttpPost(Endpoints.AppPools.StartApplicationPool)]
    public IActionResult StartApplicationPool([FromBody] AppPoolPayloadRequest appPoolName)
    {
        var pool = _serverManager.ApplicationPools[appPoolName.AppPoolName];

        if (pool == null)
        {
            return NotFound();
        }

        try
        {
            pool.Start();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

        return Ok();
    }

    [HttpPost(Endpoints.AppPools.StopApplicationPool)]
    public IActionResult StopApplicationPool([FromBody] AppPoolPayloadRequest appPoolName)
    {
        var pool = _serverManager.ApplicationPools[appPoolName.AppPoolName];

        if (pool == null)
        {
            return NotFound();
        }

        try
        {
            pool.Stop();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

        return Ok();
    }

    [HttpPost(Endpoints.AppPools.GetApplicationPoolStatus)]
    public IActionResult GetApplicationPoolStatus([FromBody] AppPoolPayloadRequest appPoolName)
    {
        var pool = _serverManager.ApplicationPools[appPoolName.AppPoolName];

        return Ok(pool.State);
    }
}
