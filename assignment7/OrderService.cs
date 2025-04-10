using System;
using System.Collections.Generic;
using System.Linq;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService
    {
        private readonly OrderContext _context;

        public OrderService(OrderContext context)
        {
            _context = context;
        }

        // 添加订单
        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        // 更新订单
        public void UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == order.OrderId);

            if (existingOrder == null)
                throw new ArgumentException("Order not found");

            // 更新订单基本信息
            existingOrder.Customer = order.Customer;
            existingOrder.OrderDate = order.OrderDate;

            // 处理订单明细
            // 先删除原有的明细
            _context.OrderDetails.RemoveRange(existingOrder.OrderDetails);

            // 添加新的明细
            foreach (var detail in order.OrderDetails)
            {
                existingOrder.AddDetail(new OrderDetail
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.ProductName,
                    UnitPrice = detail.UnitPrice,
                    Quantity = detail.Quantity
                });
            }

            _context.SaveChanges();
        }

        // 删除订单
        public void RemoveOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        // 获取所有订单
        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ToList();
        }

        // 查询订单
        public List<Order> QueryOrders(int? orderId = null, string customerName = null,
                                     string productName = null, decimal? minTotalAmount = null)
        {
            var query = _context.Orders
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (orderId.HasValue)
                query = query.Where(o => o.OrderId == orderId.Value);

            if (!string.IsNullOrEmpty(customerName))
                query = query.Where(o => o.Customer.Contains(customerName));

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(o => o.OrderDetails.Any(d => d.ProductName.Contains(productName)));

            if (minTotalAmount.HasValue)
            {
                query = query.Where(o => o.OrderDetails.Sum(d => d.UnitPrice * d.Quantity) >= minTotalAmount.Value);
            }

            return query.ToList();
        }

        // 获取单个订单
        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == orderId);
        }
    }
}