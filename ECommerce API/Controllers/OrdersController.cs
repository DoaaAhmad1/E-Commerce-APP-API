using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    
    public class OrdersController : ControllerBase
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper=mapper;
        }
        [HttpGet()]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<OrderDto>>GetOrders()
        {
            //Reminder: Get Order For One Use As Required
            var OrdersFromRepo = _orderRepository.GetOrders();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(OrdersFromRepo));
        }
        // Get Order For One Use As Required
        [HttpGet("UserOrder")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<OrderDto>> GetUserOrders()
        {
           
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).ToString();
            var User2 = UserId.Split(":");
            var User3 = User2[2].Trim();
            var OrdersFromRepo = _orderRepository.GetUserOrders(User3);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(OrdersFromRepo));
        }
        [HttpGet("{orderId}",Name ="GetOrder")]
        [Authorize(Roles = "admin, user")]
        public IActionResult GetOrdr(Guid orderId)
        {
            var OrderFromRepo = _orderRepository.GetOrder(orderId);
            if(OrderFromRepo==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderDto>(OrderFromRepo));
        }
        [HttpPost()]
        [Authorize(Roles = "admin, user")]
        public ActionResult<OrderDto>CreateOrder(OrderForUpdateAndCreateDto order)
        {
            var OrderEntity = _mapper.Map<Order>(order);
            _orderRepository.CreateOrder(OrderEntity);
            _orderRepository.Save();



            var OrderToReturn = _mapper.Map<OrderDto>(OrderEntity);
            return CreatedAtRoute("GetOrder", new { orderId = OrderToReturn.OrderId }, OrderToReturn);
        }
        [HttpDelete("{orderId}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult DeleteOrder(Guid orderId)
        {
            var OrderFromRepo = _orderRepository.GetOrder(orderId);
            if (OrderFromRepo == null)
            {
                return NotFound();
            }
            // _orderRepository.DeleteOrder(OrderFromRepo);

            //SoftDelete
            OrderFromRepo.IsDeleted = true;
            _orderRepository.Save();
            return NoContent();
        }
        [HttpPut("{orderId}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult UpdateOrder (Guid orderId,OrderForUpdateAndCreateDto order)
        {
            var OrderFromRepo = _orderRepository.GetOrder(orderId);
            if (OrderFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(order, OrderFromRepo);
            // AutoMapper Here Is Used For update  , This Function Has No Implementation
            //_orderRepository.UpdateOrder(order);
            _orderRepository.Save();
            return NoContent();
        }
    }
}
