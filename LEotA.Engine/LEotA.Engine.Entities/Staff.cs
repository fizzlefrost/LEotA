using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class Staff : Identity
    {
        public string Name { get; set; }
        public StaffRoles Role { get; set; }
        public string EmbedLink { get; set; }
    }

    public enum StaffRoles
    {
        Director, KeyMember, Member
    } 
}