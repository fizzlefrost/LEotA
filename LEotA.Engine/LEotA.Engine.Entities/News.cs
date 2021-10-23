using System;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class News: Identity
    {
        public string Description { get; set; }
        public Guid NewsColumnId { get; set; }
        public string Culture { get; set; }
    }
}