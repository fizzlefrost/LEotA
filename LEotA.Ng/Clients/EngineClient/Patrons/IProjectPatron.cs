using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IProjectPatron
    {
        public Task<CalabongaViewModel<Project>> ProjectPostAsync(ProjectCreateModel ProjectCreateModel);
        public Task<CalabongaViewModel<Project>> ProjectPutAsync(ProjectUpdateModel ProjectUpdateModel);
        public Task<CalabongaViewModel<Project>> ProjectDeleteAsync(string id);
        public Task<CalabongaViewModel<Project>> ProjectGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Project>> ProjectGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}