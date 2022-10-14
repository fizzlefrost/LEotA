using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.ProjectViewModels
{
    public class ProjectCreateViewModel : IViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string EmbedLink { get; set; }
    }
}