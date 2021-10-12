using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AboutUsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class AboutUsManager: EntityManager<AboutUsViewModel, AboutUs, AboutUsCreateViewModel, AboutUsUpdateViewModel>
    {
        public AboutUsManager(IMapper mapper, IViewModelFactory<AboutUsCreateViewModel, AboutUsUpdateViewModel> viewModelFactory, IEntityValidator<AboutUs> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}