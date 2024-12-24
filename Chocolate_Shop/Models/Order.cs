using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? AccountId { get; set; }
        public int? ProductId { get; set; }
        public int? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? AddressShipId { get; set; }
        public int? Type { get; set; }

        public virtual Account? Account { get; set; }
        public virtual AddressShip? AddressShip { get; set; }
        public virtual Product? Product { get; set; }
    }
}
