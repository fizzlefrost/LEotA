using System;
using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.EventViewModel
{
    public class EventCreateViewModel : IViewModel
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string EmbedLink { get; set; }
        public string DateTime { get; set; }
    }
}