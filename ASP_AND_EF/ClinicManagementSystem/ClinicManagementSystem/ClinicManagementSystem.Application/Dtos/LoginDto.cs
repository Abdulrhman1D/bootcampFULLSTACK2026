using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagementSystem.Application.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
