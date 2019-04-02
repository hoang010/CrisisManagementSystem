using CMS2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class EmergencyRepository
    {
        CMS2Context db = new CMS2Context();

        public List<Emergency> getEmergencies()
        {
            var all_emergencies = db.Emergencies.ToList();
            return all_emergencies;
        }

        public Emergency getEmergencyById(int? Id)
        {
            return db.Emergencies.Find(Id);
        }
    }
}