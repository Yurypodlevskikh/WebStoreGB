using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Orders;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersApiController(IOrderService OrderService) => _OrderService = OrderService;

        public Task<OrderDTO> CreateOrderAsync(string UserName, CreateOrderModel OrderModel)
        {
            return _OrderService.CreateOrderAsync(UserName, OrderModel);
        }

        public OrderDTO GetOrderById(int id)
        {
            return _OrderService.GetOrderById(id);
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return _OrderService.GetUserOrders(UserName);
        }
    }
}