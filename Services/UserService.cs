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
    public interface IUserService
    {
        IEnumerable<User> GetAll(SearchPageParameter pagingParam, ref int totalRecords);
        User GetById(int id);
        int Create(int savedUserId, User user);
        void Update(int savedUserId, User user);
        void Delete(int savedUserId, int userId);
    }

    public class UserService : IUserService
    {
        private DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll(SearchPageParameter searchParam, ref int totalResults)
        {
            IQueryable<User> allResults = _context.User.AsNoTracking();

            if (string.IsNullOrEmpty(searchParam.Search))
            {
                allResults = allResults.Where(u => !u.RemovedDate.HasValue);
            }
            else
            {
                int userId = 0;
                var search = searchParam.Search.ToUpper().Trim();
                var searchParams = search.Split(' ');
                var firstName = searchParams.Count() > 0 ? searchParams[0] : search;
                var lastName = searchParams.Count() > 1 ? searchParams[1] : search;

                if (int.TryParse(searchParam.Search, out userId))
                {
                    allResults = allResults.Where(u => u.UserId == userId);
                }
                else if (searchParams.Count() > 1)
                {
                    allResults = allResults.Where(u =>u.FirstName.ToUpper().Contains(firstName) && u.LastName.ToUpper().Contains(lastName));
                }
                else
                {
                    allResults = allResults.Where(u => u.FirstName.ToUpper().Contains(firstName) || u.LastName.ToUpper().Contains(lastName));
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

        public User GetById(int id)
        {
            var user = _context.User
                .FirstOrDefault(u => u.UserId == id);
            if (user == null)
                throw new AppException("No user with that Id!");
            return user;
        }
       
        public int Create(int savedUserId, User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }
        
        public void Update(int savedUserId, User userParam)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userParam.UserId);

            if (user == null)
                throw new AppException("User not found");
                      
            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
           
            user.SavedUserId = savedUserId;

            _context.User.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int savedUserId, int userId)
        {
            var user = _context.User.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.SavedUserId = savedUserId;
                _context.SoftDelete(user);
            }
        }
    }
}
