
using System.Data.Common;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using System.Net.Security;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private AppDbContext _context;
    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDetails> CreateOrder(OrderCreate orderCreate)
        {  
            var orderDetails = new OrderDetails();
            var order = new OrderEntity(){
                location = orderCreate.location
            };

            foreach (var id in orderCreate.ProductIds)
            {
                var product = _context.Products.SingleOrDefault(i => i.Id == id);
                if(product == null)
                return null;

                order.Products.Add(product);
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            orderDetails.Id=order.Id; 
            orderDetails.Location=order.location;
            orderDetails.Products=order.Products.Select(i => new ProductListItem
            {
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost
            }).ToList();

            return orderDetails;
        }

    public async Task<List<OrderListItem>> GetOrders()
    {
        var orders = await _context.Orders.Include(o=>o.Products).Select(s => new OrderListItem
        {
            Id = s.Id,
            location = s.location,
            Products = s.Products.Select(i => new ProductListItem
            {
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost
            }).ToList()
            
        }).ToListAsync();

        return orders;
    }

    public async Task<OrderDetails> GetOrderById(int id)
    {
        var order = await _context.Orders.Include(o=>o.Products).FirstOrDefaultAsync(c => c.Id == id);

        if (order is null) return null;

        return new OrderDetails{
            Id = order.Id,
            Location = order.location, 
            Products = order.Products.Select(i=>new ProductListItem{
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost
            }).ToList()
        };
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return false;
        else
        {
            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public async Task<OrderDetails> EditOrder(int id, OrderEdit updateorder)
        {   
            var order = await _context.Orders.Include(o=>o.Products).FirstOrDefaultAsync(c => c.Id == id);
            if (order == null) return null;
            
            order.location=updateorder.location;
            order.Products.Clear();

                foreach (var things in updateorder.ProductIds)
            {
                var product = _context.Products.SingleOrDefault(i => i.Id == things);
                if(product == null)
                return null;

                order.Products.Add(product);
            }
            await _context.SaveChangesAsync();

            return new OrderDetails{
            Id = order.Id,
            Location = order.location, 
            Products = order.Products.Select(i=>new ProductListItem{
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost
            }).ToList()
        };        
        }
}
