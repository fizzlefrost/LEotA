using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.EventViewModel
{
    public class EventUpdateViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
        public DateTime DateTime { get; set; }
    }
}