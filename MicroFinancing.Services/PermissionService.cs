using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Syncfusion.Blazor;
using Syncfusion.Blazor.RichTextEditor;

namespace MicroFinancing.Services
{
    public sealed class PermissionService : IPermissionService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IRepository<ApplicationRoleClaims, int> _roleClaimsRepository;
        private readonly ClaimsValueModel _claimsValueModel;

        public PermissionService(RoleManager<ApplicationRole> roleManager,
            IServiceScopeFactory serviceScopeFactory,
            IRepository<ApplicationRoleClaims, int> roleClaimsRepository,
            ClaimsValueModel claimsValueModel)
        {
            _roleManager = roleManager;
            _serviceScopeFactory = serviceScopeFactory;
            _roleClaimsRepository = roleClaimsRepository;
            _claimsValueModel = claimsValueModel;
        }

        public Task<object> GetPermissions(DataManagerRequest dm)
        {
            return _roleManager.Roles.Select(x => new PermissionGridDTM()
            {
                Id = x.Id,
                Name = x.Name,
                Users = x.UserRoles.Count(),
                Scopes = x.RoleClaims.Select(rc => rc.ClaimValue)
            }).ToDataResult(dm);
        }

        public async Task Update(CreateUpdatePermissionDTM item)
        {
            using var roleManager = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();
            var roles = await roleManager.FindByIdAsync(item.Id);
            roles.Name = item.Name;
            await roleManager.UpdateAsync(roles);
            var claims = _roleClaimsRepository.Entity.Where(x => x.RoleId == item.Id).ToList();
            await _roleClaimsRepository.DeleteAsync(claims);
            await _roleClaimsRepository.AddAsync(item.Scopes?.Select(x =>
            {
                var claims = _claimsValueModel.ClaimsValueModels.FirstOrDefault(c => c.Value == x);
                return new ApplicationRoleClaims()
                {
                    ClaimType = claims?.ClaimType,
                    ClaimValue = claims?.Value,
                    RoleId = item?.Id ?? string.Empty,
                };
            }));
        }

        public async Task Create(CreateUpdatePermissionDTM item)
        {
            using var roleManager = _serviceScopeFactory.CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();
            var role = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = item.Name,
            };
            var result = await roleManager.CreateAsync(role);

            if (!result.Succeeded) return;


            var claims = _roleClaimsRepository.Entity.Where(x => x.RoleId == role.Id).ToList();


            await _roleClaimsRepository.DeleteAsync(claims);
            await _roleClaimsRepository.AddAsync(item.Scopes?.Select(x =>
            {
                var claims = _claimsValueModel.ClaimsValueModels.FirstOrDefault(c => c.Value == x);
                return new ApplicationRoleClaims()
                {
                    ClaimType = claims?.ClaimType,
                    ClaimValue = claims?.Value,
                    RoleId = role?.Id ?? string.Empty,
                };
            }));
        }
    }
}