using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities.Core;

namespace LEotA.Engine.Entities
{
    public class Publication : Identity
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
        public DateTime Time { get; set; }
    }
}