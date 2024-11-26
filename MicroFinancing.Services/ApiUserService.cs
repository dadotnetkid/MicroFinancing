using AutoMapper;

using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public class ApiUserService : IUserService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApiUserService(UserManager<ApplicationUser> userManager,
                          IServiceScopeFactory serviceScopeFactory,
                          IAuthorizationService authorizationService,
                          IMapper mapper,
                          ICurrentUser currentUser)
    {
        _userManager = userManager;
        _serviceScopeFactory = serviceScopeFactory;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<CreateUpdateUserDTM> CreateUser(CreateUpdateUserDTM item)
    {
        var user = new ApplicationUser
        {
            UserName = item.UserName,
            Email = item.Email,
            FirstName = item.FirstName,
            LastName = item.LastName,
            LockoutEnabled = false,
            EmailConfirmed = true,
            AccessFailedCount = 0
        };

        await _userManager.CreateAsync(user, item.Password);

        var dtm = _mapper.Map<CreateUpdateUserDTM>(user);

        dtm.UserRole = item.UserRole;

        return dtm;
    }

    public async Task UpdateUser(CreateUpdateUserDTM user)
    {
        using var userManager = _serviceScopeFactory.CreateScope()
                                                    .ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var applicationUser = await userManager.FindByIdAsync(user.UserId);

        applicationUser.UserName = user.UserName;
        applicationUser.FirstName = user.FirstName;
        applicationUser.LastName = user.LastName;
        applicationUser.Email = user.Email;
        await userManager.UpdateAsync(applicationUser);
    }

    public async Task DeleteUser(string? userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.DeleteAsync(user);
    }

    public async Task ResetPassword(ResetPasswordUserDTM resetPasswordUserDtm)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordUserDtm.UserId);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordUserDtm.Password);
    }

    public Task<string> GetUserId()
    {

        return Task.FromResult(_currentUser.UserId ?? string.Empty);
    }

    public async Task<bool> IsAuthorizeAsync(string policy,
                                        bool showToast = true)
    {
        var res = (await _authorizationService.AuthorizeAsync(_currentUser.User, policy)).Succeeded;

        return res;
    }

    public bool IsAuthorize(string policy,
                            bool showToast = true)
    {
        throw new NotImplementedException();
    }

    public async Task AddRoles(CreateUpdateUserDTM user)
    {
        using var userManager = _serviceScopeFactory.CreateScope()
                                                    .ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var applicationUser = await userManager.FindByIdAsync(user.UserId);
        var roles = await userManager.GetRolesAsync(applicationUser);
        await userManager.RemoveFromRolesAsync(applicationUser, roles);
        await userManager.AddToRolesAsync(applicationUser, user.UserRole.AsEnumerable());
    }

    public Task<bool> IsInRoleAsync(params string[] roles)
    {
        var user = _currentUser.User;

        foreach (var role in roles)
        {
            var result = user.IsInRole(role);

            if (result)
            {
                return Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }
}
