using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Data_Access_Layer
{
    public class SummaryReportRepository
    {
        private CMS2Context db = new CMS2Context();

        public List<SummaryReport> getAllReports()
        {
            return (db.SummaryReports.ToList());
        }
        public void addNewReport(SummaryReport report)
        {
            try
            {
                db.SummaryReports.Add(report);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}