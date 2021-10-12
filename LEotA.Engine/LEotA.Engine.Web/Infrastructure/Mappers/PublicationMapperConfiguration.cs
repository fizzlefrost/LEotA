using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.PublicationViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class PublicationMapperConfiguration: MapperConfigurationBase
    {
        public PublicationMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Publication, PublicationViewModel>();
            CreateMap<IPagedList<Publication>, IPagedList<PublicationViewModel>>()
                .ConvertUsing<PagedListConverter<Publication, PublicationViewModel>>();
            
            // Writable 
            CreateMap<PublicationViewModel, Publication>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<Publication, PublicationUpdateViewModel>().ReverseMap();
            
            CreateMap<PublicationCreateViewModel, Publication>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}