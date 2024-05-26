

using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace MicroFinancing.Services
{
    public class UserAdaptor : DataAdaptor
    {
        private readonly IRepository<ApplicationUser, string> _repository;

        public UserAdaptor(IRepository<ApplicationUser, string> repository)
        {
            _repository = repository;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string? key = null)
        {
            return await _repository.Entity.AsNoTracking().Select(x => new UserGridDTM()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FirstName + " " + x.LastName,
                UserRoles = x.UserRoles.Select(ur => new ApplicationRoleDTM()
                {
                    Id = ur.Role.Id,
                    ConcurrencyStamp = ur.Role.ConcurrencyStamp,
                    Name = ur.Role.Name,
                    NormalizedName = ur.Role.NormalizedName,
                })
            }).ToDataResult(dataManagerRequest);
        }
    }
}
