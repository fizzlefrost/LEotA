using System.Collections.Generic;
using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IEventParticipantPatron
    {
        public Task<CalabongaViewModel<EventParticipant>> EventParticipantPostAsync(EventParticipantCreateModel EventParticipantCreateModel);
        public Task<CalabongaViewModel<EventParticipant>> EventParticipantPutAsync(EventParticipantUpdateModel EventParticipantUpdateModel);
        public Task<CalabongaViewModel<EventParticipant>> EventParticipantDeleteAsync(string id);
        public Task<CalabongaViewModel<EventParticipant>> EventParticipantGetByIdAsync(string id);
        public Task<CalabongaViewModel<List<EventParticipant>>> EventParticipantGetByUserIdAsync(string id);
        public Task<CalabongaGetPagedModel<EventParticipant>> EventParticipantGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}