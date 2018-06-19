using System;
using Newtonsoft.Json;

namespace AspCoreVue.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}