using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class UserRepository
    {
        CMS2Context db = new CMS2Context();

        public User getUserByName(string name)
        {
            var all_user = db.Users.ToList();
            User userDetails = null;
            foreach (User item in all_user)
            {
                if (item.UserName == name)
                {
                    userDetails = item;
                }
            }

            return userDetails;
        }
    }
}