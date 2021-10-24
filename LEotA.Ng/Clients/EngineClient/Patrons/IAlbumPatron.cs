using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IAlbumPatron
    {
        public Task<CalabongaViewModel<Album>> AlbumPostAsync(AlbumCreateModel AlbumCreateModel);
        public Task<CalabongaViewModel<Album>> AlbumPutAsync(AlbumUpdateModel AlbumUpdateModel);
        public Task<CalabongaViewModel<Album>> AlbumDeleteAsync(string id);
        public Task<CalabongaViewModel<Album>> AlbumGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<Album>> AlbumGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}