using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.PublicationViewModels
{
    public class PublicationViewModel : ViewModelBase
    {
        public string Text { get; set; }
        public byte[] PDFRaw { get; set; }
        public string EmbedLink { get; set; }
    }
}