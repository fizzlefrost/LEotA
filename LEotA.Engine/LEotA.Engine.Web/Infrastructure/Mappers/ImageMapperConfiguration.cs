using Calabonga.UnitOfWork;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.Infrastructure.Mappers.Base;
using LEotA.Engine.Web.ViewModels.ImageViewModels;

namespace LEotA.Engine.Web.Infrastructure.Mappers
{
    public class ImageMapperConfiguration : MapperConfigurationBase
    {
        public ImageMapperConfiguration()
        {
            // Readonly Controller
            CreateMap<Image, ImageViewModel>();
            CreateMap<IPagedList<Image>, IPagedList<ImageViewModel>>()
                .ConvertUsing<PagedListConverter<Image, ImageViewModel>>();

            //Writable 
            CreateMap<ImageViewModel, Image>();
            CreateMap<Image, ImageUpdateViewModel>().ReverseMap();
            
            CreateMap<ImageCreateViewModel, Image>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}