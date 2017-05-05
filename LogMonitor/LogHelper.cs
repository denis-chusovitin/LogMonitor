namespace LogMonitor
{
    public static class LogHelper
    {
        public static bool IsTaskId(string line, out int id)
        {
            //...Task id: ...
            id = 0;
            int start = line.IndexOf("Task") + 5;
            int end = line.IndexOf(": ");

            if (end < start)
            {
                return false;
            }

            return int.TryParse(line.Substring(start, end - start), out id);
        }

        public static bool IsTaskStart(string line)
        {
            return line.Contains("== Start");
        }

        public static bool IsTaskEnd(string line)
        {
            return line.Contains("== Task");
        }

        public static bool IsNodeInfo(string line)
        {
            return line.Contains("Start test");
        }

        public static bool IsTextSize(string line)
        {
            return line.Contains("Total number");
        }

        public static TaskInfo GetTaskInfo(string line, int id)
        {
            string name = line.Split('\'')[1];

            return new TaskInfo(id, name);
        }

        public static string GetNode(string line)
        {
            return line.Split('\'')[1];
        }

        public static int GetTextSize(string line)
        {
            int start = line.IndexOf("= ") + 2;
            int end = line.IndexOf(",");

            return int.Parse(line.Substring(start, end - start));
        }
    }
}
