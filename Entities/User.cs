using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreVue.Entities
{
    public partial class User : DefaultEntityProperties
    {
        public User()
        {
            UserOrder = new HashSet<UserOrder>();
            UserAddress = new HashSet<UserAddress>();
        }

        public int UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserOrder> UserOrder { get; set; }
        public virtual ICollection<UserAddress> UserAddress { get; set; }
    }
}
