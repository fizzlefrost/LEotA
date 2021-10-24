using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Event : Identity   
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
    }
}