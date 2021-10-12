using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.AlbumViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class AlbumMapperConfiguration: MapperConfigurationBase
    {
        public AlbumMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Album, AlbumViewModel>();
            CreateMap<IPagedList<Album>, IPagedList<AlbumViewModel>>()
                .ConvertUsing<PagedListConverter<Album, AlbumViewModel>>();
            
            // Writable 
            CreateMap<AlbumViewModel, Album>()
                .ForMember(x => x.Id, o => o.Ignore());
            
            CreateMap<Album, AlbumUpdateViewModel>().ReverseMap();
            
            CreateMap<AlbumCreateViewModel, Album>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}