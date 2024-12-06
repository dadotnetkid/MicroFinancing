using AutoMapper;
using MicroFinancing.Components.ToastsComponent;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public class BlazorUserService : IUserService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IToasts _toasts;
    private readonly IRepository<ApplicationUser, string> _repository;
    private readonly UserManager<ApplicationUser> _userManager;

    public BlazorUserService(UserManager<ApplicationUser> userManager,
        IServiceScopeFactory serviceScopeFactory,
        AuthenticationStateProvider authenticationStateProvider,
        IAuthorizationService authorizationService,
        IMapper mapper,
        IServiceScopeFactory scopeFactory,
        IToasts toasts,
        IRepository<ApplicationUser, string> repository)
    {
        _userManager = userManager;
        _serviceScopeFactory = serviceScopeFactory;
        _authenticationStateProvider = authenticationStateProvider;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _toasts = toasts;
        _repository = repository;
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
            AccessFailedCount = 0,
            Branch = item.Branch,
            IsEmployee = item.IsEmployee,
            BasicPay = item.BasicPay
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
        applicationUser.Branch = user.Branch;
        applicationUser.IsEmployee = user.IsEmployee;
        applicationUser.BasicPay = user.BasicPay;

        await userManager.UpdateAsync(applicationUser);
    }

    public async Task DeleteUser(string? userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        user.IsDeleted = true;

        await _userManager.UpdateAsync(user);
    }

    public async Task ResetPassword(ResetPasswordUserDTM resetPasswordUserDtm)
    {
        var user = await _userManager.FindByIdAsync(resetPasswordUserDtm.UserId);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordUserDtm.Password);
    }

    public async Task<string> GetUserId()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

        return state.User.GetUserId() ?? string.Empty;
    }

    public async Task<bool> IsAuthorizeAsync(string policy,
        bool showToast = true)
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var res = (await _authorizationService.AuthorizeAsync(state.User, policy)).Succeeded;

        if (!res && showToast)
        {
            await _toasts.ShowToast("No Permission", "You are not Authorized to do this action");
        }

        return res;
    }

    public bool IsAuthorize(string policy,
        bool showToast = true)
    {
        var state = _authenticationStateProvider.GetAuthenticationStateAsync()
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var res = (_authorizationService.AuthorizeAsync(state.User, policy)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult()).Succeeded;

        if (!res && showToast)
        {
            _toasts.ShowToast("No Permission", "You are not Authorized to do this action")
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        return res;
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

    public async Task<bool> IsInRoleAsync(params string[] roles)
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

        foreach (var role in roles)
        {
            var result = state.User.IsInRole(role);

            if (result)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<object> GetUsers(DataManagerRequest? dm)
    {
        var res = await _repository.Entity.AsNoTracking().Select(x => new UserGridDTM()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            UserName = x.UserName,
            FullName = x.FullName,
            Branch = x.Branch,
            UserRoles = x.UserRoles.Select(ur => new ApplicationRoleDTM()
            {
                Id = ur.Role.Id,
                ConcurrencyStamp = ur.Role.ConcurrencyStamp,
                Name = ur.Role.Name,
                NormalizedName = ur.Role.NormalizedName,
            })
        }).ToDataResult(dm);

        return res;
    }
}