using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataTransferObject.Users
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }
        public string? Pass { get; set; }


    }
}
