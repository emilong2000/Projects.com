using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportStore.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get;}
        void SaveOrder(Order order);
        
    }
}
