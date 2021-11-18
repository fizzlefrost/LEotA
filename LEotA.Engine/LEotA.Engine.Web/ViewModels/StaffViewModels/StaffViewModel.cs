using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.StaffViewModels
{
    public class StaffViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public StaffRoles Role { get; set; }
        public string EmbedLink { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
    }
}