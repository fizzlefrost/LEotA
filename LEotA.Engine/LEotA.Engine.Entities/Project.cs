using System;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Project : Identity
    {
        public string Text { get; set; }
        public string EmbedLink { get; set; }
        public string Culture { get; set; }
    }
}