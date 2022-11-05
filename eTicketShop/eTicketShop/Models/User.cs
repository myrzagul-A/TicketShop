using System;
using System.Collections.Generic;

namespace eTicketShop.Models
{
    public partial class User
    {
        public User()
        {
            CartItems = new HashSet<CartItem>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
