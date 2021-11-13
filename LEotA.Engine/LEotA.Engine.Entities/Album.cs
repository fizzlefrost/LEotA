using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Album : Identity
    {
        public string Name { get; set; }
        public Guid MasterId { get; set; }
        public string Author { get; set; }
    }
}