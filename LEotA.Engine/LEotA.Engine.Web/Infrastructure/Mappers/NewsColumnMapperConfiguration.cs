using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.NewsColumnViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class NewsColumnMapperConfiguration : MapperConfigurationBase
    {
        public NewsColumnMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<NewsColumn, NewsColumnViewModel>();
            CreateMap<IPagedList<NewsColumn>, IPagedList<NewsColumnViewModel>>()
                .ConvertUsing<PagedListConverter<NewsColumn, NewsColumnViewModel>>();

            //Writable 
            CreateMap<NewsColumnViewModel, NewsColumn>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<NewsColumn, NewsColumnUpdateViewModel>().ReverseMap();
            
            CreateMap<NewsColumnCreateViewModel, NewsColumn>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}