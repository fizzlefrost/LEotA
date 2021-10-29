using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface INewsPatron
    {
        public Task<CalabongaViewModel<News>> NewsPostAsync(NewsCreateModel NewsCreateModel);
        public Task<CalabongaViewModel<News>> NewsPutAsync(NewsUpdateModel NewsUpdateModel);
        public Task<CalabongaViewModel<News>> NewsDeleteAsync(string id);
        public Task<CalabongaViewModel<News>> NewsGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<News>> NewsGetPagedAsync(CalabongaGetPagedRequestModel parameters);
        public Task<int> NewsGetTotalPages(int? pageSize);
    }
}