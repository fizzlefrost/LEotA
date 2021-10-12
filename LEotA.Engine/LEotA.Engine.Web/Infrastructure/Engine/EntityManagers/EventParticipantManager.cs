using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventParticipantViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class EventParticipantManager: EntityManager<EventParticipantViewModel, EventParticipant, EventParticipantCreateViewModel, EventParticipantUpdateViewModel>
    {
        public EventParticipantManager(IMapper mapper, IViewModelFactory<EventParticipantCreateViewModel, EventParticipantUpdateViewModel> viewModelFactory, IEntityValidator<EventParticipant> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}