using Calabonga.Microservices.Core.QueryParams;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;

namespace LEotA.Engine.Web.Controllers
{
    // write only
    // [Route("api/[controller]")]
    // public class News1Controllers: ReadOnlyController<News, NewsViewModel, PagedListQueryParams>
    // {
    //     public News1Controllers(IUnitOfWork unitOfWork, IMapper mapper) 
    //         : base(unitOfWork, mapper)
    //     {
    //         
    //     }
    // }
    
    [Route("api/[controller]")]
    public class NewsController: WritableController<NewsViewModel, News, NewsCreateViewModel, NewsUpdateViewModel, PagedListQueryParams>
    {
        public NewsController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
    }
}