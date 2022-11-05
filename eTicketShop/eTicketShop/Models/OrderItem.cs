using System;
using System.Collections.Generic;

namespace eTicketShop.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
