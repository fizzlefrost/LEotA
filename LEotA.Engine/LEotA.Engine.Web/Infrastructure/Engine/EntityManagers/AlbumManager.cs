using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AlbumViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class AlbumManager: EntityManager<AlbumViewModel, Album, AlbumCreateViewModel, AlbumUpdateViewModel>
    {
        public AlbumManager(IMapper mapper, IViewModelFactory<AlbumCreateViewModel, AlbumUpdateViewModel> viewModelFactory, IEntityValidator<Album> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}