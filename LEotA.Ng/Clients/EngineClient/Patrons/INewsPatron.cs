﻿using System.Threading.Tasks;
using LEotA.Models;

namespace LEotA.Clients.EngineClient.Patrons
{
    public interface INewsPatron
    {
        public Task<CalabongaViewModel<News>> NewsGetViewModelForCreationAsync();
        public Task<CalabongaViewModel<News>> NewsPostAsync(NewsCreateModel NewsCreateModel);
        public Task<CalabongaViewModel<News>> NewsGetViewModelForEditingAsync(string id);
        public Task<CalabongaViewModel<News>> NewsPutAsync(NewsUpdateModel NewsUpdateModel);
        public Task<CalabongaViewModel<News>> NewsDeleteAsync(string id);
        public Task<CalabongaViewModel<News>> NewsGetByIdAsync(string id);
        public Task<CalabongaGetPagedModel<News>> NewsGetPagedAsync(CalabongaGetPagedRequestModel parameters);
    }
}