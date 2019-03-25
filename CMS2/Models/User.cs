using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int RoleId { get; set; }
        public UserRole Role { get; set; }
    }
}