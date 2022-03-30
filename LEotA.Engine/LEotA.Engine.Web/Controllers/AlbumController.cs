using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AlbumViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class AlbumController: WritableController<AlbumViewModel, Album, AlbumCreateViewModel, AlbumUpdateViewModel, PagedListQueryParams>
    {
        public AlbumController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
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
                int vspm (int pageSize) => (Repository.Count() % pageSize == 0) ?  Repository.Count() / pageSize : ((Repository.Count() / pageSize) + 1);
                return Ok(vspm(pageSize));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}