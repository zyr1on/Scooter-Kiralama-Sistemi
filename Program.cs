using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // database initialize edilcek
            DatabaseHelper.InitializeDatabase();

            // Baslangic olarak baslayacak form
            // MainForm() -> map olan form
            // LoginForm() -> login olma formu
            Application.Run(new LoginForm());
        }
    }
}