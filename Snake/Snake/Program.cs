using System;
using System.Collections.Generic;
using Snake.Views;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Snake.Views.Snake());
        }
    }
}
