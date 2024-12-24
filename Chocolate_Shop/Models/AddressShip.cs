using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class AddressShip
    {
        public AddressShip()
        {
            Orders = new HashSet<Order>();
        }

        public int AddressShipId { get; set; }
        public int? AccountId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
