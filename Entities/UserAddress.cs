using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreVue.Entities
{
    public partial class UserAddress
    {
        public UserAddress()
        {
            UserOrder = new HashSet<UserOrder>();
        }

        public int UserAddressId { get; set; }

        public int UserId { get; set; }
        public int AddressId { get; set; }

        
        public virtual User User {get;set;}
        public virtual ICollection<UserOrder> UserOrder { get; set; }
    }
}
