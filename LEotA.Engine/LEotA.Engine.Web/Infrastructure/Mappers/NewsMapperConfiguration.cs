using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.NewsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class NewsMapperConfiguration: MapperConfigurationBase
    {
        public NewsMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<News, NewsViewModel>();
            CreateMap<IPagedList<News>, IPagedList<NewsViewModel>>()
                .ConvertUsing<PagedListConverter<News, NewsViewModel>>();
            
            //Writable 
            CreateMap<NewsViewModel, News>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<News, NewsUpdateViewModel>().ReverseMap();
            
            CreateMap<NewsCreateViewModel, News>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}