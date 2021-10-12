using System;
using System.IO;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Image : Identity
    {
        public string Name { get; set; }
        public byte[] ImageRaw { get; set; }
        public Guid MasterId { get; set; }
    }
}