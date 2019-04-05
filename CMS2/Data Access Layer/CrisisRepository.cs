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
            var all_crises = db.Crises.OrderByDescending(x => x.TimeStamp).ToList();

            return all_crises;
        }

        public List<Crisis> getLastestCrisis()
        {
            var all_crisis = db.Crises.OrderByDescending(x => x.TimeStamp).ToList();
            List<Crisis> result = new List<Crisis>();
            foreach (var item in all_crisis)
            {
                if (DateTime.Now.Subtract(item.TimeStamp).TotalDays < 7)
                {
                    result.Add(item);
                }
            }
            return result;
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

        public List<Crisis> getCrisesByRoles(int role)
        {
            var all_crises = db.Crises.OrderBy(x => x.TimeStamp).ToList();
            List<Crisis> result = new List<Crisis>();
            if (role == 4) //scdf
            {
                foreach (var item in all_crises)
                {
                    if (item.AssistanceRequiredId == 2 || item.AssistanceRequiredId == 3|| item.AssistanceRequiredId == 5)
                    {
                        result.Add(item);
                    }
                }
            }
            else if (role == 5) //sg power
            {
                foreach (var item in all_crises)
                {
                    if (item.CategoryId == 6)
                    {
                        result.Add(item);
                    }
                }
            }
            else
            {
                Console.WriteLine("No roles found");
            }
            return result;
        }
    }
}