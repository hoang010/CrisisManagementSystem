using CMS2.Data_Access_Layer;
using CMS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.ReportAndSocialMedia_Module
{
    public class ReportJobs
    {
        public void generateReport()
        {
            CrisisRepository crisisRepository = new CrisisRepository();
            SummaryReportRepository summaryReportRepository = new SummaryReportRepository();

            var all_crisis = crisisRepository.getCrisisByTime(DateTime.Now);
            var new_report = new SummaryReport();
            if (all_crisis == null)
            {
                Console.WriteLine("no report submitted during time frame");
            }
            else
            {
                string add = "";
                foreach (var item in all_crisis)
                {
                    add += item.CallerName + item.Description;
                }
                new_report.ReportDetails = add;
                new_report.TimeStamp = DateTime.Now;
            }
            summaryReportRepository.addNewReport(new_report);
        }
    }
}