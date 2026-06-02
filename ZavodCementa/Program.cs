using System;
using System.Windows.Forms;
using ZavodCementa;

namespace ZavodCementa
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Инициализация базы данных при запуске
            DatabaseHelper.InitializeDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}