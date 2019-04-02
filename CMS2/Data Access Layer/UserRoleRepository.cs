using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class UserRoleRepository
    {
        private CMS2Context db = new CMS2Context();

        public List<UserRole> getAllRoles()
        {
            return (db.UserRoles.ToList());
        }
    }
}