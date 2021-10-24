using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IAboutUsPatron
    {
        public Task<CalabongaViewModel<AboutUs>> AboutUsPostAsync(AboutUsCreateModel aboutUsCreateModel);
        public Task<CalabongaViewModel<AboutUs>> AboutUsPutAsync(AboutUsUpdateModel aboutUsUpdateModel);
        public Task<CalabongaViewModel<AboutUs>> AboutUsDeleteAsync(string id);
        public Task<CalabongaViewModel<AboutUs>> AboutUsGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<AboutUs>> AboutUsGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}