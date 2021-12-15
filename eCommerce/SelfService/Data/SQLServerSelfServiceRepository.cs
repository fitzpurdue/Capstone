using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.interfaces;
namespace eCommerce.SelfService.Data
{
  
    public class SQLServerSelfServiceRepository : ISelfServiceRepository
    {
        private SelfServiceDbContext ctx;

        public SQLServerSelfServiceRepository(SelfServiceDbContext ctx)
        {
            this.ctx = ctx;

            // Recreate sample data per run.
            this.ctx.Customer.Clear();
            this.ctx.SaveChanges();
            this.ctx.Order.Clear();
            this.ctx.SaveChanges();
            this.ctx.OrderShipment.Clear();
            this.ctx.SaveChanges();
 

      
            SampleData.SampleCustomerData(noIdentity: true).ForEach(cust =>
            {
                this.ctx.Customer.Add(cust);
                this.ctx.SaveChanges();

            });
            SampleData.SampleOrderData(this.ctx.Customer.ToList(), noIdentity: true).ToList().ForEach(order =>
            {
                this.ctx.Order.Add(order);
                this.ctx.SaveChanges();
            });
            SampleData.SampleOrderShipmentData(this.ctx.Order.ToList(), noIdentity: true).ForEach(shipment =>
            {
                this.ctx.OrderShipment.Add(shipment);
                this.ctx.SaveChanges();
            });
           
        }
        public Customer? GetCustomer(int customerId)
        {
            var entity = this.ctx.Customer.Find(customerId);
            return entity;
        }

        public CustomerAddress? GetCustomerAddress(int customerAddressId)
        {
            return this.ctx.CustomerAddress.Find(customerAddressId);
        }

        public IList<CustomerAddress> GetCustomerAddressesByCustomer(int customerId)
        {
            return this.ctx.CustomerAddress.Where(addr => addr.CustomerId == customerId).ToList();
        }

        public IList<Customer> GetCustomers()
        {
            return this.ctx.Customer.ToList();
        }

        public Order? GetOrder(int orderId)
        {
            return this.ctx.Order.Find(orderId);
        }

        public IList<Order> GetOrders()
        {
            return this.ctx.Order.ToList();
        }

        public IList<OrderShipment> GetOrderShipments()
        {
            return this.ctx.OrderShipment.ToList();
        }

        public Customer InsertCustomer(Customer customer)
        {
            var entity = customer;
            this.ctx.Customer.Add(entity);
            this.ctx.SaveChanges();
            return entity;
        }

        public CustomerAddress InsertCustomerAddress(CustomerAddress customerAddress)
        {
            var entity = customerAddress;
            this.ctx.CustomerAddress.Add(entity);
            this.ctx.SaveChanges();
            return entity;
        }

        public Order InsertOrder(Order order)
        {
            var entity = order;
            this.ctx.Order.Add(entity);
            this.ctx.SaveChanges();
            return entity;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var entity = customer;
            var originalCustomer = this.ctx.Customer.FirstOrDefault(c => c.CustomerId == entity.CustomerId);
            this.ctx.Entry(originalCustomer).CurrentValues.SetValues(entity);
            return entity;
        }

        public CustomerAddress UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            var entity = customerAddress;
            var originalAddress = this.ctx.CustomerAddress.FirstOrDefault(ca => ca.CustomerAddressId == entity.CustomerAddressId);
            this.ctx.Entry(originalAddress).CurrentValues.SetValues(entity);
            return entity;
        }

        public Order UpdateOrder(Order order)
        {
            var entity = order;
            var  originalOrder = this.ctx.Order.FirstOrDefault(o => o.OrderId == entity.OrderId);
            this.ctx.Entry(originalOrder).CurrentValues.SetValues(entity);
            return entity;
        }
    }
}
