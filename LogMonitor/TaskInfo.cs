namespace LogMonitor
{
    public class TaskInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TextSize { get; set; }
        public string Nodes { get; set; }

        public TaskInfo(int id, string name)
        {
            Id = id;
            Name = name;
            TextSize = 0;
            Nodes = "";
        }
    }
}
