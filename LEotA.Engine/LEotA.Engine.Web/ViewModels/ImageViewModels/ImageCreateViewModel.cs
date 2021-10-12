using System;
using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace LEotA.Engine.Web.ViewModels.ImageViewModels
{
    public class ImageCreateViewModel: IViewModel
    {
        public string Name { get; set; }
        public byte[] ImageRaw { get; set; }
        public Guid MasterId { get; set; }
    }
}