using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.AboutUsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class AboutUsMapperConfiguration: MapperConfigurationBase
    {
        public AboutUsMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<AboutUs, AboutUsViewModel>();
            CreateMap<IPagedList<AboutUs>, IPagedList<AboutUsViewModel>>()
                .ConvertUsing<PagedListConverter<AboutUs, AboutUsViewModel>>();
            
            // Writable 
            CreateMap<AboutUsViewModel, AboutUs>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<AboutUs, AboutUsUpdateViewModel>().ReverseMap();
            
            CreateMap<AboutUsCreateViewModel, AboutUs>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}