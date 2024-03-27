using EgeUp.BackgroundJob.Managers.RecurringJobs;
using EgeUpBackgroundJob.Managers.RecurringJobs;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgeUpBackgroundJob.Managers.Schedules
{
    public class CheckWebsiteJob
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 0)]
        public static void DatabaseBackupOperation()
        {
            RecurringJob.RemoveIfExists(nameof(CheckWebsiteManger));
            RecurringJob.AddOrUpdate<CheckWebsiteManger>(nameof(CheckWebsiteManger),
                job => job.CheckWebsiteFailReports(), "*/1 * * * *");
        }
    }
}
