﻿using System;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Web.ViewModels.ImageViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public byte[] ImageRaw { get; set; }
        public Guid MasterId { get; set; }
    }
}