using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsColumnViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class NewsColumnController: WritableController<NewsColumnViewModel, NewsColumn, NewsColumnCreateViewModel, NewsColumnUpdateViewModel, PagedListQueryParams>
    {
        public NewsColumnController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
    }
}