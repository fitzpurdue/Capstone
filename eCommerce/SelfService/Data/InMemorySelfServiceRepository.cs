using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.interfaces;
using System.Linq;

namespace eCommerce.SelfService.Data
{
    public class NoOrderException : Exception {
        public NoOrderException(string message) : base(message) { }
    }
    public class InMemorySelfServiceRepository : ISelfServiceRepository
    {
        private IDictionary<int, Customer> customers = new Dictionary<int, Customer>();
        private IDictionary<int, Order> orders = new Dictionary<int, Order>();
       
        private IDictionary<int, OrderShipment> orderShipments = new Dictionary<int, OrderShipment>();
        private IDictionary<int, CustomerAddress> customerAddresses = new Dictionary<int, CustomerAddress>();
        
        public InMemorySelfServiceRepository()
        {
            var customers = SampleData.SampleCustomerData();
            foreach (Customer customer in customers)
            {
                this.customers.Add(customer.CustomerId, customer);
            }
            SampleData.SampleOrderData(customers).ToList().ForEach(order =>
            {
                this.orders.Add(order.OrderId, order);
            });
            SampleData.SampleOrderShipmentData(this.orders.Values.ToList()).ForEach(shipment =>
            {
                this.orderShipments.Add(shipment.OrderShipmentId, shipment);
            });

        }
        public Customer GetCustomer(int customerId)
        {
            return this.customers[customerId];
        }

        public CustomerAddress GetCustomerAddress(int customerAddressId)
        {
            return this.customerAddresses[customerAddressId];
        }

        public IList<CustomerAddress> GetCustomerAddressesByCustomer(int customerId)
        {
            var addrs = this.customerAddresses.Values.Where(addr => addr.CustomerId == customerId);
            return addrs.ToList<CustomerAddress>();
        }
        
        public Order GetOrder(int orderId)
        {
            try
            {
                return this.orders[orderId];
            }
            catch (KeyNotFoundException) {
                throw new NoOrderException($"No order for id {orderId}");
            }
        }

        public IList<Order> GetOrders()
        {
            return this.orders.Values.ToList();
        }

        public Customer InsertCustomer(Customer customer)
        {
            if (!this.customers.ContainsKey(customer.CustomerId)) {
                this.customers.Add(customer.CustomerId, customer);
            }
            return customer;
        }

        public CustomerAddress InsertCustomerAddress(CustomerAddress customerAddress)
        {
            if (!this.customerAddresses.ContainsKey(customerAddress.CustomerAddressId))
            {
                this.customerAddresses.Add(customerAddress.CustomerAddressId, customerAddress);
            }
            return customerAddress;
        }

        public Order InsertOrder(Order order)
        {
            if (!this.orders.ContainsKey(order.OrderId))
            {
                this.orders.Add(order.OrderId, order);
            }
            return order;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            this.customers[customer.CustomerId] = customer;
            return customer;
        }

        public CustomerAddress UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            this.customerAddresses[customerAddress.CustomerAddressId] = customerAddress;
            return customerAddress;
        }

        public Order UpdateOrder(Order order)
        {
            this.orders[order.OrderId] = order;
            return order;
        }

        public IList<OrderShipment> GetOrderShipments()
        {
            return this.orderShipments.Values.ToList();
        }

        public IList<Customer> GetCustomers()
        {
            return this.customers.Values.ToList();
        }
    }
}
