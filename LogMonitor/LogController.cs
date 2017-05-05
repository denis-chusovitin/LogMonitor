using System;
using System.Timers;

namespace LogMonitor
{
    public class LogController
    {
        private static Timer ticker;
        private LogView logView;
        private LogModel logModel;

        public LogController(LogView view, LogModel model)
        {
            ticker = new Timer();
            logView = view;
            logModel = model;

            ticker.Interval = 60000;
            ticker.Elapsed += UpdateView;
            ticker.Enabled = true;

            logModel.Update += view.UpdateGrid;
            logView.UpdateTasks += logModel.UpdateTasks;

            logModel.UpdateTasks();
        }

        private void UpdateView(Object source, System.Timers.ElapsedEventArgs e)
        {
            logModel.UpdateTasks();
        }
    }
}
