using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient
{
    public interface IAboutUsPatron
    {
        public Task<CalabongaViewModelForCreation> AboutUsGetViewModelForCreationAsync();
        public Task<string> AboutUsPostAsync(string text, string imageRaw);
        public Task<CalabongaViewModelForEditing<AboutUs>> AboutUsGetViewModelForEditingAsync(string id);
        public Task<string> AboutUsPutAsync(AboutUsUpdateModel aboutUsUpdateModel);
        public Task<CalabongaViewModel<AboutUs>> AboutUsGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<AboutUs>> AboutUsGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}