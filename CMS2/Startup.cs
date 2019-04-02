using CMS2.ReportAndSocialMedia_Module;
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
            ReportJobs reportJobs = new ReportJobs();
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("CMS2Context");
            app.UseHangfireDashboard();

            //add recurring report generation every 30 minutes
            RecurringJob.AddOrUpdate(() => reportJobs.sendReport(reportJobs.generateReport()), Cron.MinuteInterval(5)); //set timer here
            app.UseHangfireServer();
        }
    }
}
