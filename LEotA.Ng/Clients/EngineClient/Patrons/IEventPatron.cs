using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IEventPatron
    {
        public Task<CalabongaViewModel<Event>> EventGetViewModelForCreationAsync();
        public Task<CalabongaViewModel<Event>> EventPostAsync(EventCreateModel EventCreateModel);
        public Task<CalabongaViewModel<Event>> EventGetViewModelForEditingAsync(string id);
        public Task<CalabongaViewModel<Event>> EventPutAsync(EventUpdateModel EventUpdateModel);
        public Task<CalabongaViewModel<Event>> EventDeleteAsync(string id);
        public Task<CalabongaViewModel<Event>> EventGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Event>> EventGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}