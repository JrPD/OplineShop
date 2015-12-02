using OnlineShop.Shedule;
using Quartz;
using Quartz.Impl;

namespace OnlineShop
{
    public class UpdateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            BackUpDb.BackUp();
        }
    }

    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<UpdateJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(18, 33))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}