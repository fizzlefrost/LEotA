using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using LEotA.Engine.Entities;

namespace LEotA.Engine.Web.ViewModels.NewsColumnViewModels
{
    public class NewsColumnUpdateViewModel: ViewModelBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}