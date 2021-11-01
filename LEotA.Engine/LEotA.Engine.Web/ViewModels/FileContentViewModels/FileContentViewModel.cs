using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.FileContentViewModels
{
    public class FileContentViewModel : ViewModelBase
    {
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public Guid MasterId { get; set; }
    }
}