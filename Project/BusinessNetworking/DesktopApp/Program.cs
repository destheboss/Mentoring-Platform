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
            PasswordStrengthChecker passwordStrengthChecker = new PasswordStrengthChecker();
            IMeetingDataAccess meetingData = new MeetingDataManager();
            IPersonDataAccess personData = new PersonDataManager(meetingData, hashingManager, passwordStrengthChecker);
            IAuthenticationDataAccess authenticationDataAccess = new AuthenticationDataManager(hashingManager);
            UserManager userManager = new UserManager(personData, meetingData);
            LoggingManager loggingManager = new LoggingManager(authenticationDataAccess);
            MeetingManager meetingManager = new MeetingManager(meetingData);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new Login(userManager, meetingManager, loggingManager));
        }
    }
}