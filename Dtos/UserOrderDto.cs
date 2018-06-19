using Newtonsoft.Json;

namespace AspCoreVue.Dtos
{
    public class UserOrderDto
    {
        public int UserOrderId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public string TrackingId { get; set; }
        public AddressDto Address { get; set; }
        public UserDto User { get; set; }
    }
}