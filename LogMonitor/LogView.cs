using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogMonitor
{
    public partial class LogView : Form
    {
        public delegate void UpdateEventHandler();
        public event UpdateEventHandler UpdateTasks;

        public LogView()
        {
            InitializeComponent();
        }

        public void UpdateGrid(List<TaskInfo> tasks)
        {
            var action = (Action)(() => {
                dataGrid.DataSource = tasks;
                dataGrid.Update();
            });

            if (dataGrid.InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateTasks();
        }
    }
}
