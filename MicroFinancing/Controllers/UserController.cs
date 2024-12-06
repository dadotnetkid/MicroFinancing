using System.Text.Json;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor;

namespace MicroFinancing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost(nameof(GetUsers))]
    public async Task<ActionResult<DataResultDto<UserGridDTM>>> GetUsers([FromBody] string dm)
    {
        var dataManager = JsonSerializer.Deserialize<DataManagerRequest>(dm);

        var result = (await _userService.GetUsers(dataManager)).ToDataResultDto<UserGridDTM>();

        return Ok(result);
    }

    [HttpPost(nameof(UpdateUser))]
    public async Task<ActionResult<BaseResultDto<bool>>> UpdateUser([FromBody] CreateUpdateUserDTM item)
    {
        try
        {
            await _userService.UpdateUser(item);
            return Ok(BaseResultDto<bool>.Success(true));
        }
        catch (Exception e)
        {
            return BaseResultDto<bool>.Fail(e.Message);
        }
    }

    [HttpPut(nameof(ResetPassword))]
    public async Task<ActionResult<BaseResultDto<bool>>> ResetPassword([FromBody] ResetPasswordUserDTM item)
    {
        try
        {
            await _userService.ResetPassword(item);
            return Ok(BaseResultDto<bool>.Success(true));
        }
        catch (Exception e)
        {
            return BaseResultDto<bool>.Fail(e.Message);
        }
    }

    [HttpDelete(nameof(DeleteUser))]
    public async Task<ActionResult<BaseResultDto<bool>>> DeleteUser([FromBody] string id)
    {
        try
        {
            await _userService.DeleteUser(id);

            return BaseResultDto<bool>.Success(true);
        }
        catch (Exception e)
        {
            return BaseResultDto<bool>.Fail(e.Message);
        }
    }

    [HttpPost(nameof(CreateUser))]
    public async Task<ActionResult<BaseResultDto<CreateUpdateUserDTM>>> CreateUser([FromBody] CreateUpdateUserDTM item)
    {
        try
        {
            var res = await _userService.CreateUser(item);

            return BaseResultDto<CreateUpdateUserDTM>.Success(res);
        }
        catch (Exception e)
        {
            return BaseResultDto<CreateUpdateUserDTM>.Fail(e.Message);
        }
    }

    [HttpPost(nameof(AddRoles))]
    public async Task<ActionResult<BaseResultDto<bool>>> AddRoles([FromBody] CreateUpdateUserDTM item)
    {
        try
        {
            await _userService.AddRoles(item);

            return BaseResultDto<bool>.Success(true);
        }
        catch (Exception e)
        {
            return BaseResultDto<bool>.Fail(e.Message);
        }
    }
}