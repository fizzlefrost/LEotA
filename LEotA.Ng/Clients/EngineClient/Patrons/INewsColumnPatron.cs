using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface INewsColumnPatron
    {
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnGetViewModelForCreationAsync();
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnPostAsync(NewsColumnCreateModel NewsColumnCreateModel);
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnGetViewModelForEditingAsync(string id);
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnPutAsync(NewsColumnUpdateModel NewsColumnUpdateModel);
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnDeleteAsync(string id);
        public Task<CalabongaViewModel<NewsColumn>> NewsColumnGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<NewsColumn>> NewsColumnGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}