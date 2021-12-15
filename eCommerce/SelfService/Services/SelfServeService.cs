using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.SelfService.Data;
using eCommerce.interfaces;

namespace eCommerce.SelfService.Services
{
    public enum OrderStatus
    {
        NOT_SHIPPED,
        SHIPPED,
        CANCELLED,
        COMPLETED,

    }
    public class SelfServeService
    {
        // Would normally make private, but most business logic simple.
        public ISelfServiceRepository repo;
       
        public SelfServeService(ISelfServiceRepository repo)
        {

            this.repo = repo;

        }
        
        public IList<Customer> GetCustomers()
        {
            return this.repo.GetCustomers();
        }
        public IList<Order> GetCustomerOrders(int customerId)
        {
            var orders = this.repo.GetOrders().ToList();
            return orders.Where(o => o.CustomerId == customerId).ToList();
        }
        public IList<OrderShipment> GetOrderShipments(int orderId)
        {
            var shipments = this.repo.GetOrderShipments().Where(sh => sh.OrderId == orderId);
            return shipments.ToList();
        }
        public bool OrderCancelable(int orderId)
        {
            var shipments = this.GetOrderShipments(orderId).ToList();
            return shipments == null || shipments.All(sh => sh.DateShipped == null);
        }
        public void CancelOrder(int orderId)
        {
            if (this.OrderCancelable(orderId))
            {
                var order = this.repo.GetOrder(orderId);
                order.OrderStatus = (int)OrderStatus.CANCELLED;
                this.repo.UpdateOrder(order);
               
            }
            else
            {
                throw new OrderNotCancelable("Order has shipped.");
            }

        }
    }

    public class OrderNotCancelable : Exception
    {
        public OrderNotCancelable(string message) : base(message) { }
    }
}
