using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public int? ProductId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? CommentTime { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Product? Product { get; set; }
    }
}
