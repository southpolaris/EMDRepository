using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WifiMonitor
{
    static class Program
    {
        public static string programName;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            programName = Application.ProductName;
            Process[] list = Process.GetProcessesByName(Application.ProductName);
            if (list.Length > 1)
            {
                MessageBox.Show("同时只能运行一个应用实例");
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mMainForm = new MainForm();
            Application.Run(mMainForm);
        }
    }
}
