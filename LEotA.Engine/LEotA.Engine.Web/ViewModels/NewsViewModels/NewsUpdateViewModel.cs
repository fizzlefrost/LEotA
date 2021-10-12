using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.NewsViewModels
{
    public class NewsUpdateViewModel : ViewModelBase
    {
        public string Description { get; set; }
        public Guid NewsColumnId { get; set; }
    }
}