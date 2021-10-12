using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.EventParticipantViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class EventParticipantMapperConfiguration: MapperConfigurationBase
    {
        public EventParticipantMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<EventParticipant, EventParticipantViewModel>();
            CreateMap<IPagedList<EventParticipant>, IPagedList<EventParticipantViewModel>>()
                .ConvertUsing<PagedListConverter<EventParticipant, EventParticipantViewModel>>();
            
            // Writable 
            CreateMap<EventParticipantViewModel, EventParticipant>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<EventParticipant, EventParticipantUpdateViewModel>().ReverseMap();
            
            CreateMap<EventParticipantCreateViewModel, EventParticipant>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}