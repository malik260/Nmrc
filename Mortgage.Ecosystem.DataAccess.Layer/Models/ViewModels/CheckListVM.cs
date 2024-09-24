using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class CheckListVM
    {
        public string? Checklist { get; set; }
        //public bool AppCheckbox { get; set; }
        //public bool NotAppCheckbox { get; set; }
        public string? NhfNumber { get; set; }
        public string? Remark { get; set; }
        public string? ProductCode { get; set; }
    }

    public class SecondaryLenderCheckListVM
    {
        public string? Item { get; set; }
        //public bool AppCheckbox { get; set; }
        //public bool NotAppCheckbox { get; set; }
        public string? NhfNumber { get; set; }
        public string? Description { get; set; }
        public long PmbId { get; set; }
    }
}
