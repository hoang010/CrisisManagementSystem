using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Helpers
{
    public class LoginHelper
    {
        public bool isAuthorized(int loggedIn, List<int> roleRequired)
        {
            if (roleRequired.Contains(loggedIn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}