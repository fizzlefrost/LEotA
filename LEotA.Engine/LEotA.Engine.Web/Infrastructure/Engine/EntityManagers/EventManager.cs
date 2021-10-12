using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.EventViewModel;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class EventManager: EntityManager<EventViewModel, Event, EventCreateViewModel, EventUpdateViewModel>
    {
        public EventManager(IMapper mapper, IViewModelFactory<EventCreateViewModel, EventUpdateViewModel> viewModelFactory, IEntityValidator<Event> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}