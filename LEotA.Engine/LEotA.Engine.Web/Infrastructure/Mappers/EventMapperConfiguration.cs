using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.EventViewModel;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class EventMapperConfiguration: MapperConfigurationBase
    {
        public EventMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Event, EventViewModel>();
            CreateMap<IPagedList<Event>, IPagedList<EventViewModel>>()
                .ConvertUsing<PagedListConverter<Event, EventViewModel>>();
            
            // Writable 
            CreateMap<EventViewModel, Event>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<Event, EventUpdateViewModel>().ReverseMap();
            
            CreateMap<EventCreateViewModel, Event>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}