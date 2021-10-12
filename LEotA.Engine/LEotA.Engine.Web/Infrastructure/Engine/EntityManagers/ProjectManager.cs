using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.ProjectViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class ProjectManager: EntityManager<ProjectViewModel, Project, ProjectCreateViewModel, ProjectUpdateViewModel>
    {
        public ProjectManager(IMapper mapper, IViewModelFactory<ProjectCreateViewModel, ProjectUpdateViewModel> viewModelFactory, IEntityValidator<Project> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}