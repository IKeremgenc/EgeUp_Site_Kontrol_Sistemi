using EgeUp.BackgroundJob.Managers.RecurringJobs;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgeUp.BackgroundJob.Managers.Schedules
{
    public static class RecurringJobs
    {
        [Obsolete]
        [AutomaticRetry(Attempts = 0)]
        public static void DatabaseBackupOperation()
        {
            RecurringJob.RemoveIfExists(nameof(EndpointsJobsManger));
            RecurringJob.AddOrUpdate<EndpointsJobsManger>(nameof(EndpointsJobsManger),
                job => job.CheckWebsiteStatus(), "*/1 * * * *");
        }
    }
}
