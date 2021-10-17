using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IPublicationPatron
    {
        public Task<CalabongaViewModel<Publication>> PublicationGetViewModelForCreationAsync();
        public Task<CalabongaViewModel<Publication>> PublicationPostAsync(PublicationCreateModel PublicationCreateModel);
        public Task<CalabongaViewModel<Publication>> PublicationGetViewModelForEditingAsync(string id);
        public Task<CalabongaViewModel<Publication>> PublicationPutAsync(PublicationUpdateModel PublicationUpdateModel);
        public Task<CalabongaViewModel<Publication>> PublicationDeleteAsync(string id);
        public Task<CalabongaViewModel<Publication>> PublicationGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Publication>> PublicationGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}