using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.PublicationViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class PublicationManager: EntityManager<PublicationViewModel, Publication, PublicationCreateViewModel, PublicationUpdateViewModel>
    {
        public PublicationManager(IMapper mapper, IViewModelFactory<PublicationCreateViewModel, PublicationUpdateViewModel> viewModelFactory, IEntityValidator<Publication> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}