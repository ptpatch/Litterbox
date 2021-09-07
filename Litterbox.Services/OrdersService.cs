using Litterbox.Entities;
using LitterBox.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class OrdersService
    {
        #region Define as Singleton
        private static OrdersService _Instance;

        public static OrdersService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new OrdersService();
                }

                return (_Instance);
            }
        }

        private OrdersService()
        {
        }
        #endregion
        public bool SaveOrder(Order order)
        {
            LitterboxContext context = new LitterboxContext();

            context.Orders.Add(order);

            return context.SaveChanges() > 0;
        }
        public Order GetOrderByID(int ID)
        {
            LitterboxContext context = new LitterboxContext();

            return context.Orders.Find(ID);
        }
        public List<Order> SearchOrders(string userEmail, int? orderID, int? orderStatus, int? pageNo, int pageSize)
        {
            LitterboxContext context = new LitterboxContext();

            var orders = context.Orders.AsQueryable();

            if (orderID.HasValue && orderID.Value > 0)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                orders = orders.Where(x => x.CustomerEmail.Equals(userEmail));
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return orders.OrderByDescending(x => x.PlacedOn).Skip(skipCount).Take(pageSize).ToList();
        }
        public int SearchOrdersCount(string userEmail, int? orderID, int? orderStatus)
        {
            LitterboxContext context = new LitterboxContext();

            var orders = context.Orders.AsQueryable();

            if (orderID.HasValue && orderID.Value > 0)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                orders = orders.Where(x => x.CustomerEmail.Equals(userEmail));
            }

            if (orderStatus.HasValue && orderStatus.Value > 0)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            return orders.Count();
        }
        public bool AddOrderHistory(OrderHistory orderHistory)
        {
            LitterboxContext context = new LitterboxContext();

            context.OrderHistories.Add(orderHistory);

            return context.SaveChanges() > 0;
        }
        public List<Order> GetUserOrders(string userEmail, int? orderID, int? orderStatus, int? pageNo, int pageSize)
        {
            LitterboxContext context = new LitterboxContext();

            var orders = context.Orders.Where(x => x.CustomerEmail.Equals(userEmail));

            if (orderID.HasValue)
            {
                orders = orders.Where(x => x.ID == orderID.Value);
            }

            if (orderStatus.HasValue)
            {
                orders = orders.Where(x => x.OrderHistory.OrderByDescending(y => y.ModifiedOn).FirstOrDefault().OrderStatus == orderStatus);
            }

            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            return orders.OrderByDescending(x => x.ModifiedOn).Skip(skipCount).Take(pageSize).ToList();
        }
    }
}