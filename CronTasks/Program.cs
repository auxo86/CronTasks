using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;


namespace CronTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TaskWorker> listTasks = new List<TaskWorker>();
            for (int i = 0; i < args.Length / 2; i++)
            {
                listTasks.Add(new TaskWorker()
                {
                    iRunTimeInterval = Convert.ToInt16(args[i * 2 + 1]),
                    sTaskName = args[i * 2]
                });
            }
            
            foreach (var TaskItem in listTasks)
            {
                TaskItem.TaskTimer = new Timer(TaskItem.iRunTimeInterval * 1000);
                TaskItem.TaskTimer.AutoReset = true;
                TaskItem.BgWorker = new BackgroundWorker();
                TaskItem.BgWorker.DoWork += (sender, eventArgs) =>
                {
                    TaskItem.TaskTimer.Enabled = false;
                    Console.WriteLine($"{TaskItem.sTaskName}");
                };


                TaskItem.TaskTimer.Elapsed += (sender, eventArgs) =>
                {
                    TaskItem.BgWorker.RunWorkerAsync();
                };

                TaskItem.BgWorker.RunWorkerCompleted += (sender, eventArgs) =>
                {
                    TaskItem.TaskTimer.Enabled = true;
                    Console.WriteLine($"{TaskItem.sTaskName} complete!");
                };

                TaskItem.TaskTimer.Enabled = true;
                // System.Diagnostics.Process.Start("bash", TaskItem.sTaskName);

            }
            System.Threading.Thread.Sleep(50000);
        }
    }
}