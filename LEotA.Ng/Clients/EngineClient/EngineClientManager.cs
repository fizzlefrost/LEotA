#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using LEotA.Clients.EngineClient.Patrons;
using LEotA.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LEotA.Clients.EngineClient
{
    public class EngineClientManager
    {
        private readonly INewsPatron _newsPatron;
        private readonly IAboutUsPatron? _aboutUsPatron;
        private readonly IAlbumPatron? _albumPatron;
        private readonly IEventPatron? _eventPatron;
        private readonly IEventParticipantPatron? _eventParticipantPatron;
        private readonly IFileContentPatron? _FileContentPatron;
        // private readonly INewsPatron? _newsPatron;
        private readonly IProjectPatron? _projectPatron;
        private readonly IPublicationPatron? _publicationPatron;
        private readonly IStaffPatron? _staffPatron;

        public EngineClientManager(IServiceProvider serviceProvider, INewsPatron newsPatron)
        {
            _newsPatron = newsPatron;
            _aboutUsPatron = serviceProvider.GetService<IAboutUsPatron>();
            _albumPatron = serviceProvider.GetService<IAlbumPatron>();
            _eventPatron = serviceProvider.GetService<IEventPatron>();
            _eventParticipantPatron = serviceProvider.GetService<IEventParticipantPatron>();
            _FileContentPatron = serviceProvider.GetService<IFileContentPatron>();
            // _newsPatron = serviceProvider.GetService<INewsPatron>();
            _projectPatron = serviceProvider.GetService<IProjectPatron>();
            _publicationPatron = serviceProvider.GetService<IPublicationPatron>();
            _staffPatron = serviceProvider.GetService<IStaffPatron>();
        }
        
        public CalabongaViewModel<AboutUs>? AboutUsPost(CultureBase text, byte[] FileContentRaw) =>
            _aboutUsPatron?.AboutUsPostAsync(new AboutUsCreateModel()
            {
                Text = JsonSerializer.Serialize(text),
            }).Result;
        
        public CalabongaViewModel<AboutUs>? AboutUsPut(Guid id, CultureBase text, byte[] FileContent, Guid newId) => 
            _aboutUsPatron?.AboutUsPutAsync(new AboutUsUpdateModel()
        {
            Id = id.ToString(),
            Text = JsonSerializer.Serialize(text),
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
        
        public CalabongaViewModel<Event>? EventPost(string embedLink, CultureBase name, CultureBase text, DateTime dateTime) =>
            _eventPatron?.EventPostAsync(new EventCreateModel()
            {
                EmbedLink = embedLink,
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text),
                DateTime = dateTime.ToString("g")
            }).Result;
        
        public CalabongaViewModel<Event>? EventPut(Guid id, string embedLink, CultureBase name, CultureBase text, Guid newId, DateTime dateTime) => 
            _eventPatron?.EventPutAsync(new EventUpdateModel()
            {
                Id = id.ToString(),
                EmbedLink = embedLink,
                Name = JsonSerializer.Serialize(name),
                Text = JsonSerializer.Serialize(text),
                DateTime = dateTime.ToString("g"),
                NewId = newId.ToString()
            }).Result;
        
        public CalabongaViewModel<Event>? EventDelete(Guid id) =>
            _eventPatron?.EventDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Event>? EventGetById(Guid id) =>
            _eventPatron?.EventGetByIdAsync(id.ToString()).Result;

        // public List<Event>? EventGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
        //     bool disabledDefaultIncludes) => _eventPatron?.EventGetPagedAsync(new CalabongaGetPagedRequestModel()
        // {
        //     PageIndex = pageIndex,
        //     PageSize = pageSize,
        //     SortDirection = sortDirection,
        //     Search = search,
        //     DisabledDefaultIncludes = disabledDefaultIncludes
        // }).Result.Result.Items;

        public List<Event>? EventGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes)
        {
            var task = _eventPatron?.EventGetPagedAsync(new CalabongaGetPagedRequestModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SortDirection = sortDirection,  
                Search = search,
                DisabledDefaultIncludes = disabledDefaultIncludes
            });
            return task.GetAwaiter().GetResult().Result.Items;
        }

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
        
        public CalabongaViewModel<EventParticipant>? EventParticipantDeleteAsync(Guid id) =>
            _eventParticipantPatron?.EventParticipantDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<EventParticipant>? EventParticipantGetByIdAsync(Guid id) =>
            _eventParticipantPatron?.EventParticipantGetByIdAsync(id.ToString()).Result;

        public async Task<CalabongaViewModel<List<EventParticipant>>?> EventParticipantGetByUserIdAsync(Guid id)
        {
            var eventParticipants = await _eventParticipantPatron?.EventParticipantGetByUserIdAsync(id.ToString())!;
            return eventParticipants;
        }
        
        public List<EventParticipant>? EventParticipantGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _eventParticipantPatron?.EventParticipantGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
        
        public CalabongaViewModel<FileContent>? FileContentPost(byte[] content, Guid masterId, string mimeType, CultureBase author) =>
            _FileContentPatron?.FileContentPostAsync(new FileContentCreateModel()
            {
                Content = Convert.ToBase64String(content),
                MasterId = masterId.ToString(),
                MimeType = mimeType
            }).Result;
        
        public CalabongaViewModel<FileContent>? FileContentPut(Guid id, byte[] content, Guid masterId, Guid newId, string mimeType) => 
            _FileContentPatron?.FileContentPutAsync(new FileContentUpdateModel()
            {
                Id = id.ToString(),
                Content = Convert.ToBase64String(content),
                MasterId = masterId.ToString(),
                MimeType = mimeType,
                NewId = newId.ToString(),
            }).Result;
        
        public CalabongaViewModel<FileContent>? FileContentDelete(Guid id) =>
            _FileContentPatron?.FileContentDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<FileContent>? FileContentGetById(Guid id) =>
            _FileContentPatron?.FileContentGetByIdAsync(id.ToString()).Result;

        public List<FileContent>? FileContentGetByMasterId(Guid id) =>
            _FileContentPatron?.FileContentGetByMasterIdAsync(id.ToString()).Result;
        public List<FileContent>? FileContentGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _FileContentPatron?.FileContentGetPagedAsync(new CalabongaGetPagedRequestModel()
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

        public async Task<int> NewsGetTotalPages(int? pageSize) => _newsPatron.NewsGetTotalPages(pageSize).Result;
        public async Task<List<News>?> NewsGetPagedAsync(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes)
        {
            var newsGetPaged = await _newsPatron?.NewsGetPagedAsync(new CalabongaGetPagedRequestModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SortDirection = sortDirection,
                Search = search,
                DisabledDefaultIncludes = disabledDefaultIncludes
            })!;
            return newsGetPaged.Result.Items;
        }

        public CalabongaViewModel<Project>? ProjectPost(CultureBase name,CultureBase text, string embedLink) =>
            _projectPatron?.ProjectPostAsync(new ProjectCreateModel()
            {
                Name = JsonSerializer.Serialize(name),
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
                EmbedLink = embedLink
            }).Result;

        public CalabongaViewModel<Publication>? PublicationPut(Guid id, CultureBase text, string embedLink, byte[] PDFRaw, Guid newId) => 
            _publicationPatron?.PublicationPutAsync(new PublicationUpdateModel()
            {
                Id = id.ToString(),
                Text = JsonSerializer.Serialize(text),
                EmbedLink = embedLink,
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
        
        public CalabongaViewModel<Staff>? StaffPost(CultureBase name, string embedLink, StaffRoles role, string email, CultureBase text) =>
            _staffPatron?.StaffPostAsync(new StaffCreateModel()
            {
                Name = JsonSerializer.Serialize(name),
                EmbedLink = embedLink,
                Role = role,
                Email = email,
                Text = JsonSerializer.Serialize(text),
            }).Result;

        public CalabongaViewModel<Staff>? StaffPut(Guid id, CultureBase name, string embedLink, StaffRoles role, string email, CultureBase text ,Guid newId) => 
            _staffPatron?.StaffPutAsync(new StaffUpdateModel()
            {
                Id = id.ToString(),
                Name = JsonSerializer.Serialize(name),
                EmbedLink = embedLink,
                Role = role,
                Email = email,
                Text = JsonSerializer.Serialize(text),
                NewId = newId.ToString()
            }).Result;
        
        public CalabongaViewModel<Staff>? StaffDelete(Guid id) =>
            _staffPatron?.StaffDeleteAsync(id.ToString()).Result;
        
        public CalabongaViewModel<Staff>? StaffGetById(Guid id) =>
            _staffPatron?.StaffGetByIdAsync(id.ToString()).Result;

        public List<Staff>? StaffGetPaged(int? pageIndex, int? pageSize, int? sortDirection, string? search,
            bool disabledDefaultIncludes) => _staffPatron?.StaffGetPagedAsync(new CalabongaGetPagedRequestModel()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SortDirection = sortDirection,
            Search = search,
            DisabledDefaultIncludes = disabledDefaultIncludes
        }).Result.Result.Items;
    }
}