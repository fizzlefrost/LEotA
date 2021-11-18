using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Data;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.FileContentViewModels;
using Microsoft.AspNetCore.Http;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class FileContentManager : EntityManager<FileContentViewModel, FileContent, FileContentCreateViewModel,
        FileContentUpdateViewModel>
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork<ApplicationDbContext> _context;

        public FileContentManager(IMapper mapper, IViewModelFactory<FileContentCreateViewModel,
                FileContentUpdateViewModel> viewModelFactory, IUnitOfWork unitOfWork,
            IEntityValidator<FileContent> validator) : base(mapper, viewModelFactory, validator)
        {
            _unitOfWork = unitOfWork;
        }

        protected override IIdentity? GetIdentity() => null;

        public static async Task<FileContent> Upload(IFormFile file, string mimeType, Guid masterId, string? author)
        {
            var repository = _unitOfWork.GetRepository<FileContent>();
            FileContent fileContent = new FileContent();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                fileContent = new FileContent()
                {
                    Id = Guid.NewGuid(),
                    Content = fileBytes,
                    MasterId = masterId,
                    MimeType = mimeType,
                    Author = author
                };
                await repository.InsertAsync(fileContent);
                _unitOfWork.SaveChanges();
            }

            return fileContent;
        }
        
        public static List<FileContent> GetByMasterId(Guid masterId)
        {
            var repository = _unitOfWork.GetRepository<FileContent>();

            var ret = repository.GetAll(disableTracking: true).Where(f => f.MasterId == masterId).ToList();
            
            return ret;
        }
    }
}