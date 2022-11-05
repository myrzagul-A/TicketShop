using System;
using System.Collections.Generic;

namespace eTicketShop.Models
{
    public partial class Event
    {
        public Event()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
