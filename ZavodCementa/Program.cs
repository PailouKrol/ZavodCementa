using System;
using System.Windows.Forms;

namespace ZavodCementa
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DatabaseHelper.InitializeDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}