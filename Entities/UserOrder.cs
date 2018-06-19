using System;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreVue.Entities
{
    public partial class UserOrder : DefaultEntityProperties
    {
        public UserOrder() { }

        public int UserOrderId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string TrackingId { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
    }
}
