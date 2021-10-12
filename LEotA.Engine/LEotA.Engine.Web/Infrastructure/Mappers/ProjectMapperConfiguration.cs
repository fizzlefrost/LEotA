using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.NewsViewModels;
using LEotA.Engine.Web.ViewModels.ProjectViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class ProjectMapperConfiguration: MapperConfigurationBase
    {
        public ProjectMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Project, ProjectViewModel>();
            CreateMap<IPagedList<Project>, IPagedList<ProjectViewModel>>()
                .ConvertUsing<PagedListConverter<Project, ProjectViewModel>>();
            
            // Writable 
            CreateMap<ProjectViewModel, Project>()
                .ForMember(x => x.Id, o => o.Ignore());
            CreateMap<Project, ProjectUpdateViewModel>().ReverseMap();
            
            CreateMap<ProjectCreateViewModel, Project>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}