using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class RateProduct
    {
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int? Rate { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
