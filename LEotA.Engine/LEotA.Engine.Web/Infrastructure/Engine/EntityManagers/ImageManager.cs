using System.Security.Principal;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.ImageViewModels;
using LEotA.Engine.Web.ViewModels.NewsViewModels;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class ImageManager: EntityManager<ImageViewModel, Image, ImageCreateViewModel, ImageUpdateViewModel>
    {
        public ImageManager(IMapper mapper, IViewModelFactory<ImageCreateViewModel, ImageUpdateViewModel> viewModelFactory, IEntityValidator<Image> validator) : base(mapper, viewModelFactory, validator)
        {
        
        }

        protected override IIdentity? GetIdentity() => null;
    }
}