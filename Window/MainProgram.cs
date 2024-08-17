using NewPetProjectC_.SimpleWork;
using System;
using System.Windows.Forms;

namespace NewPetProjectC_
{
    internal static class MainProgram
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var Context = new EntityContext())
            {
                Application.Run(new MainWindow(Context));
            }
        }
    }
}
