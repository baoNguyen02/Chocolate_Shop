using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class Account
    {
        public Account()
        {
            AddressShips = new HashSet<AddressShip>();
            ChatBoxes = new HashSet<ChatBox>();
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            RateProducts = new HashSet<RateProduct>();
        }

        public int AccountId { get; set; }
        public int? RoleId { get; set; }
        public string? Gmail { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? AccountImage { get; set; }
        public int? Status { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<AddressShip> AddressShips { get; set; }
        public virtual ICollection<ChatBox> ChatBoxes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RateProduct> RateProducts { get; set; }
    }
}
