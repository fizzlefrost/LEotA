using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities.Core;

namespace LEotA.Engine.Entities
{
    public class Publication : Identity
    {
        public string Text { get; set; }
        public byte[] PDFRaw { get; set; }
        public string EmbedLink { get; set; }
    }
}