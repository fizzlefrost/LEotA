using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsColumnViewModels;
using LEotA.Engine.Web.ViewModels.NewsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class NewsColumnManager: EntityManager<NewsColumnViewModel, NewsColumn, NewsColumnCreateViewModel, NewsColumnUpdateViewModel>
    {
        public NewsColumnManager(IMapper mapper, IViewModelFactory<NewsColumnCreateViewModel, NewsColumnUpdateViewModel> viewModelFactory, IEntityValidator<NewsColumn> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}