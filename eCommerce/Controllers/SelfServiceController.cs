using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.SelfService.Data;
using eCommerce.SelfService.Services;
using eCommerce.SelfService;

namespace eCommerce.Controllers
{
    [ApiController]
    [Route("/customers")]
    public class SelfServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SelfServiceController> _logger;
        private readonly SelfServeService service;

        public SelfServiceController(SelfServeService service, ILogger<SelfServiceController> logger)
        {
            _logger = logger;
            this.service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetCustomers());
        }

        [Route("{id}/orders")]
        [HttpGet]
        public IActionResult Get(int customerId)
        {
            return Ok(service.GetCustomerOrders(customerId));
        }

        [HttpGet]
        [Route("/orders")]
        public IActionResult GetOrders()
        {
            return Ok(service.repo.GetOrders());
        }
        [HttpGet]
        [Route("/orderShipments")]
        public IActionResult GetOrderShipments()
        {
            return Ok(service.repo.GetOrderShipments());
        }
        [HttpPut]
        [Route("{customerId}/orders/{orderId}")]
        public IActionResult Put(int customerId, int orderId, Order newOrder)
        {
            Order order;
            try
            {
                order = service.repo.GetOrder(orderId);
            } 
            catch (NoOrderException ex)
            {
                return BadRequest(ex.Message);
            }
            
            if (newOrder.CustomerId != customerId)
            {
                return BadRequest("Invalid Customer Id");
            }
            if (order.OrderStatus != newOrder.OrderStatus)
            {
                if (newOrder.OrderStatus == (int)OrderStatus.CANCELLED)
                {
                    try
                    {
                        service.CancelOrder(orderId);
                        return Ok();
                    }
                    catch (OrderNotCancelable)
                    {
                        return BadRequest("Order has already shipped");
                    }
                    
                }
            }
    
            service.repo.UpdateOrder(newOrder);

            
            return Ok();
        }
    }
}
