using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.AlbumViewModels
{
    public class AlbumCreateViewModel : IViewModel
    {
        public string Name { get; set; }
        public Guid MasterId { get; set; }
        public string Author { get; set; }
    }
}