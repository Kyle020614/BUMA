using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUMA.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string AccessLevel { get; set; } // "Admin" or "User"
    }
}
