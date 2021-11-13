using System;
using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.AlbumViewModels
{
    public class AlbumViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public Guid MasterId { get; set; }
        public string Author { get; set; }
    }
}