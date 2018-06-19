using System;
using System.Collections.Generic;
using System.Linq;
using AspCoreVue.Entities;
using System.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using AspCoreVue.Contexts;
using System.IO;
using System.ComponentModel;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using AspCoreVue.Helpers;

namespace AspCoreVue.Services
{
    public interface IOrderService
    {
        IEnumerable<UserOrder> GetAll(SearchPageParameter pagingParam, ref int totalRecords);
        UserOrder GetById(int orderId);
        int Create(int savedUserId, UserOrder order);
        void Update(int savedUserId, UserOrder order);
        void Delete(int orderId, int savedUserId);
        IEnumerable<UserOrder> GetAllUserOrders(int userId);
    }

    public class OrderService : IOrderService
    {
        private DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<UserOrder> GetAll(SearchPageParameter searchParam, ref int totalResults)
        {
            IQueryable<UserOrder> allResults = _context.UserOrder
                .Include(o => o.Address)
                .Include(o => o.User)
                .AsNoTracking();

            if (string.IsNullOrEmpty(searchParam.Search))
            {
                allResults = allResults                    
                    .Where(u => !u.RemovedDate.HasValue);
            }
            else
            {
                int orderId = 0;
                var search = searchParam.Search.ToUpper().Trim();
                var searchParams = search.Split(' ');
                var firstName = searchParams.Count() > 0 ? searchParams[0] : search;
                var lastName = searchParams.Count() > 1 ? searchParams[1] : search;

                if (int.TryParse(searchParam.Search, out orderId))
                {
                    allResults = allResults.Where(u => u.UserOrderId == orderId);
                }
                else if (searchParams.Count() > 1)
                {
                    allResults = allResults.Where(u =>u.User.FirstName.ToUpper().Contains(firstName) && u.User.LastName.ToUpper().Contains(lastName));
                }
                else
                {
                    allResults = allResults.Where(u => u.User.FirstName.ToUpper().Contains(firstName) || u.User.LastName.ToUpper().Contains(lastName));
                }
            }

            totalResults = allResults.Count();

            if ((searchParam.PageNumber - 1) * searchParam.MaxRowsPerPage > totalResults)
            {
                searchParam.PageNumber = (totalResults / searchParam.MaxRowsPerPage) - 1;
                searchParam.PageNumber = searchParam.PageNumber <= 0 ? 1 : searchParam.PageNumber;
            }

            return allResults.Skip((searchParam.PageNumber - 1) * searchParam.MaxRowsPerPage).Take(searchParam.MaxRowsPerPage);
        }

        public IEnumerable<UserOrder> GetAllUserOrders(int userId)
        {
            IQueryable<UserOrder> allResults = _context.UserOrder
                .Include(o => o.Address)
                .Include(o => o.User)
                .Where(u => !u.RemovedDate.HasValue && u.UserId == userId)
                .AsNoTracking();

            return allResults;
        }

        public UserOrder GetById(int orderId)
        {
            var order = _context.UserOrder
                .Include(u => u.User)
                .Include(u => u.Address)
                .FirstOrDefault(u => u.UserOrderId == orderId);
            if (order == null)
                throw new AppException("No order with that Id!");
            return order;
        }
       
        public int Create(int savedUserId, UserOrder order)
        {
            _context.UserOrder.Add(order);
            _context.SaveChanges();

            return order.UserOrderId;
        }
        
        public void Update(int savedUserId, UserOrder userParam)
        {
            var order = _context.UserOrder
                    .Include(u => u.Address)
                    .FirstOrDefault(u => u.UserOrderId == userParam.UserOrderId);

            if (order == null)
                throw new AppException("Order not found");

            // update order properties
            order.TrackingId = userParam.TrackingId;

            order.Address.AddressLine1 = userParam.Address.AddressLine1;
            order.Address.AddressLine2 = userParam.Address.AddressLine2;
            order.Address.City = userParam.Address.City;
            order.Address.StateCode = userParam.Address.StateCode;
            order.Address.ZipCode = userParam.Address.ZipCode;

            order.SavedUserId = savedUserId;

            _context.UserOrder.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int userOrderId, int savedUserId)
        {
            var userOrder = _context.UserOrder.FirstOrDefault(u => u.UserOrderId == userOrderId);
            if (userOrder != null)
            {
                userOrder.SavedUserId = savedUserId;
                _context.SoftDelete(userOrder);
            }
        }
    }
}
