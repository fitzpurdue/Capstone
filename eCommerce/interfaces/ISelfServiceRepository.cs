using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.SelfService;

namespace eCommerce.interfaces
{
    public interface ISelfServiceRepository
    {
        public IList<Order> GetOrders();
        public Order? GetOrder(int orderId);
        public Order UpdateOrder(Order order);
        public Order InsertOrder(Order order);

        public IList<OrderShipment> GetOrderShipments();

        public IList<Customer> GetCustomers();
        public Customer? GetCustomer(int customerId);
        public Customer UpdateCustomer(Customer customer);
        public Customer InsertCustomer(Customer customer);


        public IList<CustomerAddress> GetCustomerAddressesByCustomer(int customerId);
        public CustomerAddress? GetCustomerAddress(int customerAddressId);
        public CustomerAddress UpdateCustomerAddress(CustomerAddress customerAddress);
        public CustomerAddress InsertCustomerAddress(CustomerAddress customerAddress);


        
    }
}
