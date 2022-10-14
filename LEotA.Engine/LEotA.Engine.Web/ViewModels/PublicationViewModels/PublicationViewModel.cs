using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.PublicationViewModels
{
    public class PublicationViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
        public DateTime Time { get; set; }
    }
}