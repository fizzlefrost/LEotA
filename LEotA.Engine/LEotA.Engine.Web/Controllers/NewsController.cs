using System;
using System.Linq;
using System.Threading.Tasks;
using Calabonga.Microservices.Core.QueryParams;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using Microsoft.AspNetCore.Authorization;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class NewsController: WritableController<NewsViewModel, News, NewsCreateViewModel, NewsUpdateViewModel, PagedListQueryParams>
    {
        public NewsController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
        
        
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> TotalPages([FromQuery] int pageSize)
        {
            try
            {
                if (pageSize == 0) pageSize = 1;
                int vspm (int pageSize) => (Repository.Count() % pageSize == 0) ? ((Repository.Count() / pageSize) + 1) : Repository.Count() / pageSize;
                return Ok(vspm(pageSize));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}