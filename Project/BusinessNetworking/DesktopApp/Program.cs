using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using DesktopApp.Forms;
using System;
using System.Windows.Forms;
using DataAccessLayer.Managers;

namespace DesktopApp.Forms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            HashingManager hashingManager = new HashingManager();
            ISessionDataAccess sessionData = new SessionDataManager();
            IPersonDataAccess personData = new PersonDataManager(sessionData, hashingManager);
            UserManager userManager = new UserManager(personData);
            SessionManager sessionManager = new SessionManager(sessionData);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new Login(userManager, sessionManager));
        }
    }
}