using System;
using System.ComponentModel;
using System.Timers;

namespace CronTasks
{
    public class TaskWorker
    {
        public int iRunTimeInterval { get; set; }
        public string sTaskName { get; set; }
        public Timer TaskTimer { get; set; }
        public BackgroundWorker BgWorker { get; set; }
    }
}