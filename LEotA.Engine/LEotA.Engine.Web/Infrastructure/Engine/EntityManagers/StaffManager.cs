using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.StaffViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class StaffManager: EntityManager<StaffViewModel, Staff, StaffCreateViewModel, StaffUpdateViewModel>
    {
        public StaffManager(IMapper mapper, IViewModelFactory<StaffCreateViewModel, StaffUpdateViewModel> viewModelFactory, IEntityValidator<Staff> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}