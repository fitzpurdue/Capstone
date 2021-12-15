using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.SelfService
{

    public class Order
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
       
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }


    }
    public class CustomerAddress
    {
        public int CustomerAddressId { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
    public class OrderShipment
    {
        public int OrderShipmentId { get; set; }
        public int OrderId { get; set; }
        public string ShipmentAddress { get; set; }
        public DateTime? DateShipped { get; set; }
    }
}
