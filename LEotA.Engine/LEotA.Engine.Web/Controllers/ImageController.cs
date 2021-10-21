using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.ImageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImageController: WritableController<ImageViewModel, Image, ImageCreateViewModel, ImageUpdateViewModel, PagedListQueryParams>
    {
        public ImageController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
        }
        
        // [Authorize]
        // [Route("[action]")]
        // public async Task<IActionResult> GetImageByMasterIdAsync(Guid id)
        // {
        //     try
        //     {
        //         var images = Repository.GetAll(true).Where(i => i.MasterId == id);
        //         return Ok(images);
        //     }
        //     catch (Exception e)
        //     {
        //         return StatusCode(500, "Internal Server Error");
        //     }
        // }
    }
}