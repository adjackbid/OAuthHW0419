namespace OAuthHW
{
    internal static class Program
    {
        public static Main MainForm { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainForm = new Main();
            Application.Run(MainForm);
        }
    }
}