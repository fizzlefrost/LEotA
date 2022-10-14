using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.AlbumViewModels
{
    public class AlbumUpdateViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public Guid MasterId { get; set; }
        public string Author { get; set; }
    }
}