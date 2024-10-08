﻿namespace Mortgage.Ecosystem.BusinessLogic.Layer.Models.Data
{
    public class TreeNodeState
    {
        public bool Opened { get; set; }
        public bool Disabled { get; set; }
        public bool Selected { get; set; }

        public static TreeNodeState Default = new TreeNodeState
        {
            Opened = false,
            Disabled = false,
            Selected = false
        };
    }
}
