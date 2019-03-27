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

        public Crisis getCrisisById(int? id)
        {
            var crisis = db.Crises.Find(id);

            return crisis;
        }

        public List<Crisis> getCrisisByTime(DateTime time)
        {
            var now = DateTime.Now;
            var all_crisis = db.Crises.ToList();
            List<Crisis> result = new List<Crisis>();
            foreach (var item in all_crisis)
            {
                if (time.Subtract(now).TotalMinutes < 30)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public List<AssistanceRequired> GetAssistanceRequired()
        {
            var all_assistance_required = db.AssistanceRequireds.ToList();
            return all_assistance_required;
        }
        public List<Category> GetCategories()
        {
            var all_categories = db.Categories.ToList();
            return all_categories;
        }
        public List<Emergency> GetEmergencies()
        {
            var all_emergencies = db.Emergencies.ToList();
            return all_emergencies;
        }


    }
}