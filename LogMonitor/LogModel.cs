using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;

namespace LogMonitor
{
    public class LogModel
    {
        public delegate void UpdateTableEventHandler(List<TaskInfo> tasks);
        public event UpdateTableEventHandler Update;

        private List<TaskInfo> activeTasks;

        const string fileAddress = "\\\\lts1-srv01\\d$\\MTS\\DiffTaskSvr\\!Logs\\admLog\\LT01 difftasksvr.log"; 

        public LogModel()
        {
            activeTasks = new List<TaskInfo>();
        }

        public void ReadLog()
        {
            var fs = new FileStream(fileAddress, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var file = new StreamReader(fs))
            {
                string line = file.ReadLine();
                int id;

                while ((line != null))
                {
                    if (LogHelper.IsTaskId(line, out id))
                    {
                        if (LogHelper.IsTaskStart(line))
                        {
                            activeTasks.Add(LogHelper.GetTaskInfo(line, id));
                        }
                        else if (LogHelper.IsTaskEnd(line))
                        {
                            activeTasks = activeTasks.Where(x => x.Id != id).ToList();
                        }
                        else if (LogHelper.IsNodeInfo(line))
                        {
                            var task = activeTasks.Find(x => x.Id == id);

                            if (task != null)
                            {
                                if (task.Nodes.Length != 0)
                                {
                                    task.Nodes += ", ";
                                }

                                task.Nodes += LogHelper.GetNode(line);
                            }
                        }
                        else if (LogHelper.IsTextSize(line))
                        {
                            var task = activeTasks.Find(x => x.Id == id);

                            if (task != null)
                            {
                                task.TextSize = LogHelper.GetTextSize(line);
                            }
                        }
                    }

                    line = file.ReadLine();
                }
            };
        }

        public void UpdateTasks()
        {
            ReadLog();

            Update(activeTasks);
        }
    }
}
