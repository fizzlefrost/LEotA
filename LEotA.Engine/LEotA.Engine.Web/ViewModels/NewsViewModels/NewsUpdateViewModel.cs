﻿using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.NewsViewModels
{
    public class NewsUpdateViewModel : ViewModelBase
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }
    }
}