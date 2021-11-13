using System;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class News: Identity
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }
    }
}