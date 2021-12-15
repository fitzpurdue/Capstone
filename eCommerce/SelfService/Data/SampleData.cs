using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using eCommerce.SelfService.Services;

namespace eCommerce.SelfService.Data
{

    public static class SampleData
    {
        
     
        public static List<Customer> SampleCustomerData(bool noIdentity = false)
        {
            List<Customer> container = new List<Customer>();
            List<string> custNames = new List<string>() { "John", "Bob" };
            for (int i = 0; i < custNames.Count; i++)
            {
                Customer cust = new Customer { Name = custNames[i], Phone = "555-555-5555" };
                if (!noIdentity)
                {
                    cust.CustomerId = i;
                }
                container.Add(cust);
            }
            return container;
        }
        public static List<Item> SampleItemData(bool noIdentity = false)
        {
            List<Item> container = new List<Item>();
            List<string> itemNames = new List<string>() { 
                "DVD", "SpongeBob", "Toy Car", "Skate", "Roller Blade",
            };
            for (int i = 0; i < itemNames.Count; i++)
            {
                Item item = new Item { Name = itemNames[i], Price = 5.00M, Sku = "123-abc" };
                if (!noIdentity)
                {
                    item.ItemId = i;
                }
                container.Add(item);
            }
            return container;

        }
        public static List<Order> SampleOrderData(IList<Customer> customers, bool noIdentity = false)
        {
            List<Order> container = new List<Order>();
            customers.ToList().ForEach(cust =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Order order = new Order()
                    {
                        CustomerId = cust.CustomerId,
                        OrderDate = DateTime.Now,
                        OrderStatus = new Random().Next(0, 4)
                    };
                    if (!noIdentity)
                    {
                        order.OrderId = new Random().Next(0, 1000);
                    }
                    container.Add(order);

                }
            });
            return container;
        }
        public static List<OrderShipment> SampleOrderShipmentData(IList<Order> orders, bool noIdentity = false)
        {
            List<OrderShipment> container = new List<OrderShipment>();
            orders.ToList().ForEach(order =>
            {
                if (order.OrderStatus != (int)OrderStatus.NOT_SHIPPED)
                {
                    OrderShipment shipment = new OrderShipment()
                    {
                        OrderId = order.OrderId,        
                        DateShipped = DateTime.Now,
                        ShipmentAddress = "555 Evergreen Terrace"

                    };
                    if (!noIdentity)
                    {
                        shipment.OrderShipmentId = new Random().Next(0, 100);
                    }
                    container.Add(shipment);
                }
            });
            return container;
        }
      
    }
}
