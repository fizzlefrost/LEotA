using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IStaffPatron
    {
        public Task<CalabongaViewModel<Staff>> StaffPostAsync(StaffCreateModel StaffCreateModel);
        public Task<CalabongaViewModel<Staff>> StaffPutAsync(StaffUpdateModel StaffUpdateModel);
        public Task<CalabongaViewModel<Staff>> StaffDeleteAsync(string id);
        public Task<CalabongaViewModel<Staff>> StaffGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Staff>> StaffGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}