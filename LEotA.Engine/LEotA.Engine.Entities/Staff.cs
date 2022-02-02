using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Staff : Identity
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public StaffRoles Role { get; set; }
        public string EmbedLink { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }

    public enum StaffRoles
    {
        Director, KeyMember, Member
    } 
}