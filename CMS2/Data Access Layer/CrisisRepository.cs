using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public bool editCrisis(Crisis crisis)
        {
            try
            {
                crisis.TimeStamp = DateTime.Now;
                db.Entry(crisis).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public Crisis removeCrisis(int? id)
        {
            Crisis crisis = db.Crises.Find(id);
            try
            {
                db.Crises.Remove(crisis);
                db.SaveChanges();
                return crisis;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return crisis;
            }
        }

        public List<Crisis> getCrisisByTime(DateTime time)
        {
            var all_crisis = db.Crises.ToList();
            List<Crisis> result = new List<Crisis>();
            foreach (var item in all_crisis)
            {
                if (time.Subtract(item.TimeStamp).TotalMinutes < 30)
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

        public bool addCrisis(Crisis crisis)
        {
            try
            {
                db.Crises.Add(crisis);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}