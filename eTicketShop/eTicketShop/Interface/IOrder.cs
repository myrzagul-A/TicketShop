using eTicketShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketShop.Interface
{
    public interface IOrder
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId,  decimal TotalPrice, DateTime PaidDateTime);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string UserId, string userRole);
    }
}
