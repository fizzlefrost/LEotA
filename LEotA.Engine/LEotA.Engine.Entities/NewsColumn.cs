﻿using System;
using System.ComponentModel.DataAnnotations;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace LEotA.Engine.Entities
{
    public class NewsColumn : Identity
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}