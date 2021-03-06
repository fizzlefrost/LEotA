using System;
using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.EventViewModel
{
    public class EventViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
        public DateTime DateTime { get; set; }
    }
}