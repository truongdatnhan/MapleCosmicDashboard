using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int Amount { get; set; }
        public bool Used { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RedeemDate { get; set; }

    }
}
