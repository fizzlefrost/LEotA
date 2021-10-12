using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class NewsManager: EntityManager<NewsViewModel, News, NewsCreateViewModel, NewsUpdateViewModel>
    {
        public NewsManager(IMapper mapper, IViewModelFactory<NewsCreateViewModel, NewsUpdateViewModel> viewModelFactory, IEntityValidator<News> validator) : base(mapper, viewModelFactory, validator)
        {
            
        }

        protected override IIdentity? GetIdentity() => null;
    }
}