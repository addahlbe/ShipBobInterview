using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreVue.Entities
{
    public partial class Address : DefaultEntityProperties
    {
        public Address()
        {
            UserOrder = new HashSet<UserOrder>();
            UserAddress = new HashSet<UserAddress>();
        }

        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CellPhone { get; set; }
        
        public virtual ICollection<UserAddress> UserAddress {get;set;}
        public virtual ICollection<UserOrder> UserOrder { get; set; }
    }
}
