using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Staff : Identity
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string EmbedLink { get; set; }
    }
}