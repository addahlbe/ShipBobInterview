using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspCoreVue.Services;
using AspCoreVue.Entities;
using AspCoreVue.Helpers;
using AutoMapper;
using AspCoreVue.Dtos;

namespace AspCoreVue.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IOrderService userOrderService;
        private IMapper mapper;

        public OrderController(
           IOrderService _userOrderService, IMapper _mapper)
        {
            userOrderService = _userOrderService;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAllOrders([FromQuery] SearchPageParameter searchParam)
        {
            try
            {
                int totalRecords = 0;
                var orders = mapper.Map<IList<UserOrderDto>>(userOrderService.GetAll(searchParam, ref totalRecords));
                return Ok(new
                {
                    orders,
                    totalRecords
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetAllUserOrders(int userId)
        {
            try
            {
                var orders = mapper.Map<IList<UserOrderDto>>(userOrderService.GetAllUserOrders(userId));
                return Ok(orders);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        public IActionResult GetById(int orderId)
        {
            try
            {
                return Ok(mapper.Map<UserOrderDto>(userOrderService.GetById(orderId)));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromBody]UserOrder formOrder)
        {
            formOrder.SavedDate = DateTime.Now;
            try
            {
                // save 
                return Ok(userOrderService.Create(0, formOrder));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{userId}")]
        public IActionResult Update([FromBody]UserOrder formOrder, int userId)
        {
            formOrder.UserId = userId;
            formOrder.SavedDate = DateTime.Now;
            try
            {
                // save 
                userOrderService.Update(0, formOrder);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }
    }
}
