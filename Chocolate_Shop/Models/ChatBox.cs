using System;
using System.Collections.Generic;

namespace Chocolate_Shop.Models
{
    public partial class ChatBox
    {
        public ChatBox()
        {
            Chats = new HashSet<Chat>();
        }

        public int ChatBoxId { get; set; }
        public int? AccountId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
