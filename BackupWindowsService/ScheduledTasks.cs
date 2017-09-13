using NLog.Internal;
using Quartz;
using Quartz.Impl;
using ScheduledTasks.BackupAndRestore;
using ScheduledTasksLibrary.BackupAndRestore;
using System;
using System.Threading;

namespace BackupWindowsService
{
    public class ScheduledTasks : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            NlogDefinition.logger.Info("Zamanlanmış Görevler başlatıldı");
            PostgreSQLBackup postgreSqlBackup = new PostgreSQLBackup();

            /* PostgreSqlBackup.PgDump ve PostgreSqlRestore.PgRestore metodunun parametreleri */

            string serverName = System.Configuration.ConfigurationManager.AppSettings["serverName"];
            string port = System.Configuration.ConfigurationManager.AppSettings["port"];
            string databaseName = System.Configuration.ConfigurationManager.AppSettings["databaseName"];
            string userName = System.Configuration.ConfigurationManager.AppSettings["userName"];
            string password = System.Configuration.ConfigurationManager.AppSettings["password"];
            string path = System.Configuration.ConfigurationManager.AppSettings["path"];
            string pathToFileZip = System.Configuration.ConfigurationManager.AppSettings["pathToFileZip"];

            /* SendMail.SendMailBackupInformation metodunun parametreleri*/

            string senderAddress = System.Configuration.ConfigurationManager.AppSettings["senderAddress"];
            string senderAddressPassword = System.Configuration.ConfigurationManager.AppSettings["senderAddressPassword"];
            string sentAddress = System.Configuration.ConfigurationManager.AppSettings["sentAddress"];
            string hostAddress = System.Configuration.ConfigurationManager.AppSettings["hostAddress"];
            string subject1 = System.Configuration.ConfigurationManager.AppSettings["subject1"];
            string subject2 = System.Configuration.ConfigurationManager.AppSettings["subject2"];
            string body1 = System.Configuration.ConfigurationManager.AppSettings["body1"];
            string body2 = System.Configuration.ConfigurationManager.AppSettings["body2"];
            
            if (postgreSqlBackup.PgDump(serverName, port, databaseName, userName, password, path) == true)
            {
                FileOperations dosyaIslemleri = new FileOperations();
                if (dosyaIslemleri.FileZip(pathToFileZip, databaseName) == true)
                {
                    dosyaIslemleri.Delete(databaseName+" "+String.Format("{0:d}", DateTime.Now)+".backup", path );
                    SendMail sendMail = new SendMail();
                  //  sendMail.SendMailBackupInformation(senderAddress, senderAddressPassword, sentAddress, hostAddress, subject1, body1);
                }
                else
                {
                    SendMail sendMail = new SendMail();
                  //  sendMail.SendMailBackupInformation(senderAddress, senderAddressPassword, sentAddress, hostAddress, subject2, body2);
                }
            }
            else
            {
                SendMail sendMail = new SendMail();
               // sendMail.SendMailBackupInformation(senderAddress, senderAddressPassword, sentAddress, hostAddress, subject2, body2);
            }
        }
        public void TriggerTask() //Tetikleyici
        {
            IScheduler sched = Start();
            IJobDetail ScheduledTasks = JobBuilder.Create<ScheduledTasks>().WithIdentity("ScheduledTasks", null).Build();
            ISimpleTrigger TriggerGorev = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("ScheduledTasks").StartAt(DateTime.Now).WithSimpleSchedule(x => x.WithIntervalInHours(720).RepeatForever()).Build();
            sched.ScheduleJob(ScheduledTasks, TriggerGorev);
        }
        public IScheduler Start() //Zamanlayıcı 
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            if (!sched.IsStarted)
                sched.Start();
            return sched;
        }
    }
}
