using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
namespace MTPosInMa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.OfficeSkins.Register();
           // DevExpress.UserSkins.BonusSkins.Register();
            //UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            //UserLookAndFeel.Default.SetSkinStyle("Blue");
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            MTPosInMa.Model.SysPar.Start();
            Application.Run(new frmMain());
        }
    }
}
