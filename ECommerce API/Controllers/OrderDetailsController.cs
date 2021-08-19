using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId}/orderDetails")]
   
    public class OrderDetailsController :ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepositort;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderDetailsController(IOrderDetailRepository orderDetailRepositort, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderDetailRepositort = orderDetailRepositort;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<OrderDetailDto>>GetOrderDetailsForOrder (Guid orderId)
        {
            var OrderFromRepo = _orderRepository.GetOrder(orderId);
            if(OrderFromRepo==null)
            {
                return NotFound();
            }
            var OrderDetailsForOrderFromRepo = _orderDetailRepositort.GetOrderDetails(orderId);
            return Ok(_mapper.Map<IEnumerable<OrderDetailDto>>(OrderDetailsForOrderFromRepo));
        }

    }
}
