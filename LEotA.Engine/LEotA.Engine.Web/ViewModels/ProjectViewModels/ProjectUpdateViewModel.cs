using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.ProjectViewModels
{
    public class ProjectUpdateViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
    }
}