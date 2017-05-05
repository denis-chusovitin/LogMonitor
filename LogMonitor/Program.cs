using System;
using System.Windows.Forms;

namespace LogMonitor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LogView logView = new LogView();
            LogModel logModel = new LogModel();

            LogController controller = new LogController(logView, logModel);

            Application.Run(logView);
        }
    }
}
