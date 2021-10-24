using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class AboutUs : Identity
    {
        public string Text { get; set; }
        public byte[] Image { get; set; }
    }
}