using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspCoreVue.Services;
using AspCoreVue.Entities;
using AspCoreVue.Helpers;
using AutoMapper;
using AspCoreVue.Controllers;
using AspCoreVue.Dtos;

namespace AspCoreVue.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;
        private IMapper mapper;

        public UserController(IUserService _userService, IMapper _mapper)
        {
            userService = _userService;
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers([FromQuery] SearchPageParameter searchParam)
        {
            try
            {
                int totalRecords = 0;
                var users = mapper.Map<IList<UserDto>>(userService.GetAll(searchParam, ref totalRecords));
                return Ok(new
                {
                    users,
                    totalRecords
                });
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(int userId)
        {
            try
            {
                return Ok(mapper.Map<UserDto>(userService.GetById(userId)));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromBody]User formUser)
        {
            formUser.SavedDate = DateTime.Now;
            try
            {
                // save 
                return Ok(userService.Create(10000, formUser));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(int userId)
        {
            userService.Delete(10000, userId);
            return Ok();
        }

        [HttpPut("{userId}")]
        public IActionResult Update([FromBody]User formUser, int userId)
        {
            formUser.UserId = userId;
            formUser.SavedDate = DateTime.Now;
            try
            {
                // save 
                userService.Update(10000, formUser);
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
