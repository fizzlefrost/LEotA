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
            _projectPatron = serviceProvider.GetService<IProjectPatron>();
            _publicationPatron = serviceProvider.GetService<IPublicationPatron>();
        }
        
        public CalabongaViewModel<AboutUs>? AboutUsPost(CultureBase text, byte[] imageRaw) =>
            _aboutUsPatron?.AboutUsPostAsync(new AboutUsCreateModel()
            {
                Text = JsonSerializer.Serialize(text),
                Image = Convert.ToBase64String(imageRaw),
            }).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsPut(Guid id, CultureBase text, byte[] image, Guid newId) => 
            _aboutUsPatron?.AboutUsPutAsync(new AboutUsUpdateModel()
        {
            Id = id.ToString(),
            Text = JsonSerializer.Serialize(text),
            Image = Convert.ToBase64String(image),
            NewId = newId.ToString()
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
        
        public CalabongaViewModel<Album>? AlbumPost(CultureBase name) =>
            _albumPatron?.AlbumPostAsync(new AlbumCreateModel()
            {
                Name = JsonSerializer.Serialize(name)
            }).Result;
        
        public CalabongaViewModel<Album>? AlbumPut(Guid id, CultureBase name, Guid newId) => 
            _albumPatron?.AlbumPutAsync(new AlbumUpdateModel()
            {
                Id = id.ToString(),
                Name = JsonSerializer.Serialize(name),
                NewId = newId.ToString()
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
        
        public CalabongaViewModel<Event>? EventPost(string embedLink, CultureBase name, CultureBase text) =>
            _eventPatron?.EventPostAsync(new EventCreateModel()
            {
                EmbedLink = embedLink,
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text)
            }).Result;
        
        public CalabongaViewModel<Event>? EventPut(Guid id, string embedLink, CultureBase name, CultureBase text, Guid newId) => 
            _eventPatron?.EventPutAsync(new EventUpdateModel()
            {
                Id = id.ToString(),
                EmbedLink = embedLink,
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text),
                NewId = newId.ToString(),
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
        
        public CalabongaViewModel<EventParticipant>? EventParticipantPost(Guid eventId, Guid userId) =>
            _eventParticipantPatron?.EventParticipantPostAsync(new EventParticipantCreateModel()
            {
                EventId = eventId.ToString(),
                UserId = userId.ToString()
            }).Result;

        public CalabongaViewModel<EventParticipant>? EventParticipantPut(Guid id, Guid eventId, Guid userId, Guid newId) => 
            _eventParticipantPatron?.EventParticipantPutAsync(new EventParticipantUpdateModel()
            {
                Id = id.ToString(),
                EventId = eventId.ToString(),
                UserId = userId.ToString(),
                NewId = newId.ToString()
            }).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantDelete(Guid id) =>
            _eventParticipantPatron?.EventParticipantDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantGetByIdAsync(Guid id) =>
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
        public CalabongaViewModel<Image>? ImagePost(byte[] imageRaw, Guid masterId) =>
            _imagePatron?.ImagePostAsync(new ImageCreateModel()
            {
                ImageRaw = Convert.ToBase64String(imageRaw),
                MasterId = masterId.ToString()
            }).Result;
        
        public CalabongaViewModel<Image>? ImagePut(Guid id, byte[] imageRaw, Guid masterId, Guid newId) => 
            _imagePatron?.ImagePutAsync(new ImageUpdateModel()
            {
                Id = id.ToString(),
                ImageRaw = Convert.ToBase64String(imageRaw),
                MasterId = masterId.ToString(),
                NewId = newId.ToString(),
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
        
        public CalabongaViewModel<News>? NewsPost(CultureBase description, CultureBase text, CultureBase name) =>
            _newsPatron?.NewsPostAsync(new NewsCreateModel()
            {
                Description = JsonSerializer.Serialize(description),
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text)
            }).Result;
        
        public CalabongaViewModel<News>? NewsPut(Guid id, CultureBase description, CultureBase name, CultureBase text, Guid newId) => 
            _newsPatron?.NewsPutAsync(new NewsUpdateModel()
            {
                Id = id.ToString(),
                Description = JsonSerializer.Serialize(description),
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text),
                NewId = newId.ToString()
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

        public CalabongaViewModel<Project>? ProjectPost(CultureBase text, string embedLink) =>
            _projectPatron?.ProjectPostAsync(new ProjectCreateModel()
            {
                Text = JsonSerializer.Serialize(text),
                EmbedLink = embedLink
            }).Result;

        public CalabongaViewModel<Project>? ProjectPut(Guid id, CultureBase text, string embedLink, Guid newId) => 
            _projectPatron?.ProjectPutAsync(new ProjectUpdateModel()
            {
                Id = id.ToString(),
                Text = JsonSerializer.Serialize(text),
                EmbedLink = embedLink,
                NewId = newId.ToString()
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

        public CalabongaViewModel<Publication>? PublicationPost(CultureBase text, string embedLink, byte[] PDFRaw) =>
            _publicationPatron?.PublicationPostAsync(new PublicationCreateModel()
            {
                Text = JsonSerializer.Serialize(text),
                EmbedLink = embedLink,
                PDFRaw = Convert.ToBase64String(PDFRaw)
            }).Result;

        public CalabongaViewModel<Publication>? PublicationPut(Guid id, CultureBase text, string embedLink, byte[] PDFRaw, Guid newId) => 
            _publicationPatron?.PublicationPutAsync(new PublicationUpdateModel()
            {
                Id = id.ToString(),
                Text = JsonSerializer.Serialize(text),
                EmbedLink = embedLink,
                PDFRaw = Convert.ToBase64String(PDFRaw),
                NewId = newId.ToString()
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