﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class TitleListParam
    {
        public string? Name { get; set; }

        [NotMapped]
        public List<int>? Ids { get; set; }
    }
}
