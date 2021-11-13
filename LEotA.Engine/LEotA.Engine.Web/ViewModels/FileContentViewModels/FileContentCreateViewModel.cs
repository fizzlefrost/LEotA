using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.FileContentViewModels
{
    public class FileContentCreateViewModel : IViewModel
    {
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public Guid MasterId { get; set; }
        public string Author { get; set; }
        public FileType FileType { get; set; }
    }
}