using System;
using System.Collections.Generic;

namespace eTicketShop.Models
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
