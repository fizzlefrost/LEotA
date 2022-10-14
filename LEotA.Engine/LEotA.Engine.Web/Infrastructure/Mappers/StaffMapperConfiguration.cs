using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.StaffViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class StaffMapperConfiguration: MapperConfigurationBase
    {
        public StaffMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Staff, StaffViewModel>();
            CreateMap<IPagedList<Staff>, IPagedList<StaffViewModel>>()
                .ConvertUsing<PagedListConverter<Staff, StaffViewModel>>();
            
            // Writable 
            CreateMap<StaffViewModel, Staff>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<Staff, StaffUpdateViewModel>().ReverseMap();
            
            CreateMap<StaffCreateViewModel, Staff>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}