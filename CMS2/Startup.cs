using CMS2.Data_Access_Layer;
using CMS2.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(CMS2.Startup))]
namespace CMS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("CMS2Context");
            app.UseHangfireDashboard();
            //var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            RecurringJob.AddOrUpdate(() => generateReport(), Cron.MinuteInterval(5));
            app.UseHangfireServer();
        }

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
            }
            summaryReportRepository.addNewReport(new_report);
        }
    }
}
