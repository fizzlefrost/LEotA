using AutoMapper;
using Calabonga.Microservices.Core.QueryParams;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Controllers;
using Calabonga.UnitOfWork.Controllers.Factories;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.AboutUsViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LEotA.Engine.Web.Controllers
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
    public class AboutUsController: WritableController<AboutUsViewModel, AboutUs, AboutUsCreateViewModel, AboutUsUpdateViewModel, PagedListQueryParams>
    {
        public AboutUsController(IEntityManagerFactory entityManagerFactory, IUnitOfWork unitOfWork, IMapper mapper) 
            : base(entityManagerFactory, unitOfWork, mapper)
        {
            
        }

        // [Authorize(Policy = "Logs:UserRoles:View", Roles = AppData.SystemAdministratorRoleName)]
        // [Route("[action]")]
        // public string Secret()
        // {
        //     return "Secret string from Orders APaI";
        // }
    }
}