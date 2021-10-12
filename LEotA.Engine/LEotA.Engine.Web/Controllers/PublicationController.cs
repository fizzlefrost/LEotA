using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.PublicationViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class PublicationController: WritableController<PublicationViewModel, Publication, PublicationCreateViewModel, PublicationUpdateViewModel, PagedListQueryParams>
    {
        public PublicationController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
    }
}