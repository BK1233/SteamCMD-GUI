using System;
using System.Windows.Forms;

namespace SteamCMD_GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SteamCMDMainMenu());
        }
    }
}
