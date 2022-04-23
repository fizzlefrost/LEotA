using System.Collections.Generic;
using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface IFileContentPatron
    {
        public Task<CalabongaViewModel<FileContent>> FileContentPostAsync(FileContentCreateModel FileContentCreateModel);
        public Task<CalabongaViewModel<FileContent>> FileContentPutAsync(FileContentUpdateModel FileContentUpdateModel);
        public Task<CalabongaViewModel<FileContent>> FileContentDeleteAsync(string id);
        public Task<CalabongaViewModel<FileContent>> FileContentGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<FileContent>> FileContentGetPagedAsync(CalabongaGetPagedRequestModel parameters);
        public Task<List<FileContent>> FileContentGetByMasterIdAsync(string id);
        public Task<FileContent> FileContentGetByMasterIdOneAsync(string id);
    }
}