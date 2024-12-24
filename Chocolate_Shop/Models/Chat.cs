using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class Chat
    {
        public int ChatId { get; set; }
        public int? ChatBoxId { get; set; }
        public DateTime? ChatTime { get; set; }
        public string? Content { get; set; }
        public string? ChatBy { get; set; }

        public virtual ChatBox? ChatBox { get; set; }
    }
}
