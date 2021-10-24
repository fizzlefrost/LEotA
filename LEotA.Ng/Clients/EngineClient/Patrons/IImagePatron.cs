using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IImagePatron
    {
        public Task<CalabongaViewModel<Image>> ImagePostAsync(ImageCreateModel ImageCreateModel);
        public Task<CalabongaViewModel<Image>> ImagePutAsync(ImageUpdateModel ImageUpdateModel);
        public Task<CalabongaViewModel<Image>> ImageDeleteAsync(string id);
        public Task<CalabongaViewModel<Image>> ImageGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Image>> ImageGetPagedAsync(CalabongaGetPagedRequestModel parameters);
        public Task<CalabongaGetPagedModel<Image>> ImageGetByMasterIdAsync(string id);
    }
}