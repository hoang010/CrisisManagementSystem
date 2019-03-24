using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class CrisisRepository
    {
        private CMS2Context db = new CMS2Context();

        public List<Crisis> getAllCrises()
        {
            var all_crises = db.Crises.ToList();

            return all_crises;
        }
    }
}