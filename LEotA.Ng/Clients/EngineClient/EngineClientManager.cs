#nullable enable
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient.Patrons;
using LEotA.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Clients.EngineClient
{
    public class EngineClientManager
    {
        private readonly IAboutUsPatron? _aboutUsPatron;
        private readonly IAlbumPatron? _albumPatron;
        private readonly IEventPatron? _eventPatron;
        private readonly IEventParticipantPatron? _eventParticipantPatron;
        private readonly IImagePatron? _imagePatron;
        private readonly INewsPatron? _newsPatron;
        private readonly INewsColumnPatron? _newsColumnPatron;
        private readonly IProjectPatron? _projectPatron;
        private readonly IPublicationPatron? _publicationPatron;

        public EngineClientManager(IServiceProvider serviceProvider)
        {
            _aboutUsPatron = serviceProvider.GetService<IAboutUsPatron>();
            _albumPatron = serviceProvider.GetService<IAlbumPatron>();
            _eventPatron = serviceProvider.GetService<IEventPatron>();
            _eventParticipantPatron = serviceProvider.GetService<IEventParticipantPatron>();
            _imagePatron = serviceProvider.GetService<IImagePatron>();
            _newsPatron = serviceProvider.GetService<INewsPatron>();
            _newsColumnPatron = serviceProvider.GetService<INewsColumnPatron>();
            _projectPatron = serviceProvider.GetService<IProjectPatron>();
            _publicationPatron = serviceProvider.GetService<IPublicationPatron>();
        }

        public CalabongaViewModel<AboutUs>? AboutUsGetViewModelForCreation() =>
            _aboutUsPatron?.AboutUsGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsPost(string text, string imageRaw, string culture) =>
            _aboutUsPatron?.AboutUsPostAsync(new AboutUsCreateModel()
            {
                Text = text,
                Image = imageRaw,
                Culture = culture
            }).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsPostGetViewModelForEditing(string id) =>
            _aboutUsPatron?.AboutUsGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsPut(string id, string text, string image, string culture, string newId) => 
            _aboutUsPatron?.AboutUsPutAsync(new AboutUsUpdateModel()
        {
            Id = id,
            Text = text,
            Image = image,
            NewId = newId,
            Culture = culture
        }).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsDelete(Guid id) =>
            _aboutUsPatron?.AboutUsDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsGetById(Guid id) =>
            _aboutUsPatron?.AboutUsGetByIdAsync(id.ToString()).Result;

        public List<AboutUs>? AboutUsGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _aboutUsPatron?.AboutUsGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<Album>? AlbumGetViewModelForCreation() =>
            _albumPatron?.AlbumGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<Album>? AlbumPost(string name) =>
            _albumPatron?.AlbumPostAsync(new AlbumCreateModel()
            {
                Name = name
            }).Result;
        
        public CalabongaViewModel<Album>? AlbumPostGetViewModelForEditing(string id) =>
            _albumPatron?.AlbumGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<Album>? AlbumPut(string id, string name, string newId) => 
            _albumPatron?.AlbumPutAsync(new AlbumUpdateModel()
            {
                Id = id,
                Name = name,
                NewId = newId
            }).Result;
        
        public CalabongaViewModel<Album>? AlbumDelete(Guid id) =>
            _albumPatron?.AlbumDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Album>? AlbumGetById(Guid id) =>
            _albumPatron?.AlbumGetByIdAsync(id.ToString()).Result;

        public List<Album>? AlbumGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _albumPatron?.AlbumGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<Event>? EventGetViewModelForCreation() =>
            _eventPatron?.EventGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<Event>? EventPost(string embedLink, string name, string text) =>
            _eventPatron?.EventPostAsync(new EventCreateModel()
            {
                EmbedLink = embedLink,
                Name = name,
                Text = text
            }).Result;
        
        public CalabongaViewModel<Event>? EventPostGetViewModelForEditing(string id) =>
            _eventPatron?.EventGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<Event>? EventPut(string id, string embedLink, string name, string text, string newId) => 
            _eventPatron?.EventPutAsync(new EventUpdateModel()
            {
                Id = id,
                EmbedLink = embedLink,
                Name = name,
                Text = text,
                NewId = newId,
            }).Result;
        
        public CalabongaViewModel<Event>? EventDelete(Guid id) =>
            _eventPatron?.EventDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Event>? EventGetById(Guid id) =>
            _eventPatron?.EventGetByIdAsync(id.ToString()).Result;

        public List<Event>? EventGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _eventPatron?.EventGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantGetViewModelForCreation() =>
            _eventParticipantPatron?.EventParticipantGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantPost(string eventId, string userId) =>
            _eventParticipantPatron?.EventParticipantPostAsync(new EventParticipantCreateModel()
            {
                EventId = new Guid(eventId),
                UserId = new Guid(userId)
            }).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantPostGetViewModelForEditing(string id) =>
            _eventParticipantPatron?.EventParticipantGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantPut(string id, string eventId, string userId, string newId) => 
            _eventParticipantPatron?.EventParticipantPutAsync(new EventParticipantUpdateModel()
            {
                Id = new Guid(id),
                EventId = new Guid(eventId),
                UserId = new Guid(userId),
                NewId = new Guid(newId)
            }).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantDelete(Guid id) =>
            _eventParticipantPatron?.EventParticipantDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantGetById(Guid id) =>
            _eventParticipantPatron?.EventParticipantGetByIdAsync(id.ToString()).Result;

        public List<EventParticipant>? EventParticipantGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _eventParticipantPatron?.EventParticipantGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<Image>? ImageGetViewModelForCreation() =>
            _imagePatron?.ImageGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<Image>? ImagePost(byte[] imageRaw, string masterId, string name) =>
            _imagePatron?.ImagePostAsync(new ImageCreateModel()
            {
                ImageRaw = imageRaw,
                MasterId = new Guid(masterId),
                Name = name
            }).Result;
        
        public CalabongaViewModel<Image>? ImagePostGetViewModelForEditing(string id) =>
            _imagePatron?.ImageGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<Image>? ImagePut(string id, byte[] imageRaw, string masterId, string name, string newId) => 
            _imagePatron?.ImagePutAsync(new ImageUpdateModel()
            {
                Id = id,
                ImageRaw = imageRaw,
                MasterId = new Guid(masterId),
                Name = name,
                NewId = newId,
            }).Result;
        
        public CalabongaViewModel<Image>? ImageDelete(Guid id) =>
            _imagePatron?.ImageDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Image>? ImageGetById(Guid id) =>
            _imagePatron?.ImageGetByIdAsync(id.ToString()).Result;

        public CalabongaGetPagedModel<Image>? ImageGetByMasterId(Guid id) =>
            _imagePatron?.ImageGetByMasterIdAsync(id.ToString()).Result;
        public List<Image>? ImageGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _imagePatron?.ImageGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<News>? NewsGetViewModelForCreation() =>
            _newsPatron?.NewsGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<News>? NewsPost(string description, string newsColumnId, string culture) =>
            _newsPatron?.NewsPostAsync(new NewsCreateModel()
            {
                Description = description,
                NewsColumnId = new Guid(newsColumnId),
                Culture = culture
            }).Result;
        
        public CalabongaViewModel<News>? NewsPostGetViewModelForEditing(string id) =>
            _newsPatron?.NewsGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<News>? NewsPut(string id, string description, string newsColumnId, string newId, string culture) => 
            _newsPatron?.NewsPutAsync(new NewsUpdateModel()
            {
                Id = id,
                Description = description,
                NewsColumnId = new Guid(newsColumnId),
                NewId = newId,
                Culture = culture
            }).Result;
        
        public CalabongaViewModel<News>? NewsDelete(Guid id) =>
            _newsPatron?.NewsDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<News>? NewsGetById(Guid id) =>
            _newsPatron?.NewsGetByIdAsync(id.ToString()).Result;

        public List<News>? NewsGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _newsPatron?.NewsGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnGetViewModelForCreation() =>
            _newsColumnPatron?.NewsColumnGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnPost(string text, string name) =>
            _newsColumnPatron?.NewsColumnPostAsync(new NewsColumnCreateModel()
            {
                Text = text,
                Name = name
            }).Result;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnPostGetViewModelForEditing(string id) =>
            _newsColumnPatron?.NewsColumnGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnPut(string id, string text, string name, string newId) => 
            _newsColumnPatron?.NewsColumnPutAsync(new NewsColumnUpdateModel()
            {
                Id = id,
                Text = text,
                Name = name,
                NewId = newId,
            }).Result;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnDelete(Guid id) =>
            _newsColumnPatron?.NewsColumnDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<NewsColumn>? NewsColumnGetById(Guid id) =>
            _newsColumnPatron?.NewsColumnGetByIdAsync(id.ToString()).Result;

        public List<NewsColumn>? NewsColumnGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _newsColumnPatron?.NewsColumnGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<Project>? ProjectGetViewModelForCreation() =>
            _projectPatron?.ProjectGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<Project>? ProjectPost(string text, string embedLink) =>
            _projectPatron?.ProjectPostAsync(new ProjectCreateModel()
            {
                Text = text,
                EmbedLink = embedLink
            }).Result;
        
        public CalabongaViewModel<Project>? ProjectPostGetViewModelForEditing(string id) =>
            _projectPatron?.ProjectGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<Project>? ProjectPut(string id, string text, string embedLink, string newId) => 
            _projectPatron?.ProjectPutAsync(new ProjectUpdateModel()
            {
                Id = id,
                Text = text,
                EmbedLink = embedLink,
                NewId = newId,
            }).Result;
        
        public CalabongaViewModel<Project>? ProjectDelete(Guid id) =>
            _projectPatron?.ProjectDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Project>? ProjectGetById(Guid id) =>
            _projectPatron?.ProjectGetByIdAsync(id.ToString()).Result;

        public List<Project>? ProjectGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _projectPatron?.ProjectGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<Publication>? PublicationGetViewModelForCreation() =>
            _publicationPatron?.PublicationGetViewModelForCreationAsync().Result;
        
        public CalabongaViewModel<Publication>? PublicationPost(string text, string embedLink, byte[] PDFRaw) =>
            _publicationPatron?.PublicationPostAsync(new PublicationCreateModel()
            {
                Text = text,
                EmbedLink = embedLink,
                PDFRaw = PDFRaw
            }).Result;
        
        public CalabongaViewModel<Publication>? PublicationPostGetViewModelForEditing(string id) =>
            _publicationPatron?.PublicationGetViewModelForEditingAsync(id).Result;
        
        public CalabongaViewModel<Publication>? PublicationPut(string id, string text, string embedLink, byte[] PDFRaw, string newId) => 
            _publicationPatron?.PublicationPutAsync(new PublicationUpdateModel()
            {
                Id = id,
                Text = text,
                EmbedLink = embedLink,
                PDFRaw = PDFRaw,
                NewId = newId,
            }).Result;
        
        public CalabongaViewModel<Publication>? PublicationDelete(Guid id) =>
            _publicationPatron?.PublicationDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Publication>? PublicationGetById(Guid id) =>
            _publicationPatron?.PublicationGetByIdAsync(id.ToString()).Result;

        public List<Publication>? PublicationGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _publicationPatron?.PublicationGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
    }
}