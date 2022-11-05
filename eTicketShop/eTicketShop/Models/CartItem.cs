using System;
using System.Collections.Generic;

namespace eTicketShop.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Qty { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
