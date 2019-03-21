using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class CrisisRepository
    {
        private readonly CMS2Context _dbcontext;

        public List<Crisis> getAllCrises(CMS2Context _dbcontext)
        {
            var all_crises = _dbcontext.Crises.ToList();

            return all_crises;
        }
    }
}