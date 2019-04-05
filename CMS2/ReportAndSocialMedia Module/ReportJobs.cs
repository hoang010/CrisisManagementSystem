using CMS2.Data_Access_Layer;
using CMS2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CMS2.ReportAndSocialMedia_Module
{
    public class ReportJobs
    {
        private SummaryReportRepository summaryReportRepository = new SummaryReportRepository();
        private CrisisRepository crisisRepository = new CrisisRepository();
  
        //sendcrisis for approval when a level 3 crisis is made
        public void sendCrisis(Crisis crisis)
        {
            Console.WriteLine("Crisis Level 3");
            var new_report = new SummaryReport();
            new_report.TimeStamp = DateTime.Now;
            string add = "";

            add += "Report for Crisis Id: " + crisis.Id +
                        "\nCaller Name: " + crisis.CallerName +
                        "\nCaller Number: " + crisis.CallerNumber +
                        "\nLocation: " + crisis.Location +
                        "\nDescription: " + crisis.Description +
                        "\nCategory: " + crisis.Category.Description +
                        "\nAssistance Required: " + crisis.AssistanceRequired.Assistance+
                        "\nEmergency Level: " + crisis.Emergency.Level+
                        "\nDate and Time: " + crisis.TimeStamp;
            new_report.ReportDetails = add;
            summaryReportRepository.addNewReport(new_report);

        }

        //send the report by email
        public void sendReport(SummaryReport report)
        {
            try
            {
                //api usage
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://cmsntu.herokuapp.com/email");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new
                    {
                        to = "crisis.management.system.2019@gmail.com",
                        subject = "Summary Report at: " + DateTime.Now.ToString(),
                        message = report.ReportDetails
                    });
                    Console.WriteLine(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //generate the report when requested (timing identified in startup.cs)
        public SummaryReport generateReport()
        {
            var all_crisis = crisisRepository.getCrisisByTime(DateTime.Now);
            var new_report = new SummaryReport();
            if (all_crisis.Count() == 0)
            {
                new_report.ReportDetails = "No new report submitted during time frame";
                new_report.TimeStamp = DateTime.Now;
                new_report.Approved = true;
            }
            else
            {
                string add = "";
                foreach (var item in all_crisis)
                {
                    add += "Crisis ID: " + item.Id +
                        "\nCaller Name: " + item.CallerName +
                        "\nCaller Number: " + item.CallerNumber +
                        "\nLocation: " + item.Location +
                        "\nDescription: " + item.Description +
                        "\nCategory: " + item.Category.Description +
                        "\nAssistance Required: " + item.AssistanceRequired.Assistance +
                        "\nEmergency Level: " + item.Emergency.Level +
                        "\nDate and Time: " + item.TimeStamp;
                }
                new_report.ReportDetails = add;
                new_report.TimeStamp = DateTime.Now;
                new_report.Approved = true;
            }
            summaryReportRepository.addNewReport(new_report);
            return new_report;
        }
    }
}