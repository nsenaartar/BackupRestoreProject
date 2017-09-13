using NLog;
using Quartz;
using Quartz.Impl;
using ScheduledTasksLibrary.BackupAndRestore;
using System;
using System.ServiceProcess;

namespace BackupWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            Process();
        }
        protected override void OnStop()
        {
           NlogDefinition.logger.Info("Servis durduruldu.");
        }
        public void Process()
        {
            NlogDefinition.logger.Info("Servis başlatıldı.");
            ScheduledTasks zamanlanmısGorevler = new ScheduledTasks();
            zamanlanmısGorevler.TriggerTask();
        }
        
    }
}
