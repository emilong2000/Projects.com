using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext application;
        public EFOrderRepository(ApplicationDbContext context)
        {
            application = context;
        }

        public IQueryable<Order> Orders => application.Orders
            .Include(p=>p.lines)
            .ThenInclude(p=>p.Product);

        public void SaveOrder(Order order)
        {
            application.AttachRange(order.lines.Select(p => p.Product));
            if (order.OrderId == 0)
            {
                application.Orders.Add(order);
            }     
            application.SaveChanges();
        }
        
    }
}
