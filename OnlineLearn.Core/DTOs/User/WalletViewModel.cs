using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.DTOs
{
    public class ChargeWalletVM
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class WalletVM
    {
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
