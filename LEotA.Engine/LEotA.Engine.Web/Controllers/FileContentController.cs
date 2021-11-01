using System;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Engine.EntityManagers;
using LEotA.Engine.Web.ViewModels.FileContentViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    public class FileContentController: WritableController<FileContentViewModel, FileContent, FileContentCreateViewModel, FileContentUpdateViewModel, PagedListQueryParams>
    {
        public FileContentController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="masterId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
#pragma warning disable 1998
        public async Task<IActionResult> Upload(IFormFile file, Guid masterId, string mimeType)
#pragma warning restore 1998
        {
            try
            {
                var uploadedFile = await FileContentManager.Upload(file, masterId, mimeType);
                return Ok(uploadedFile);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
#pragma warning disable 1998
        public async Task<IActionResult> FileByMasterIdAsync(Guid masterId)
#pragma warning restore 1998
        {
            try
            {
                var file = FileContentManager.GetByMasterId(masterId);
                return Ok(file);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}