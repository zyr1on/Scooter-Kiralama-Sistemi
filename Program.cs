using Scooter_Kiralama_Sistemi.API;
using Scooter_Kiralama_Sistemi.API.Routes;
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

            var timer = new System.Threading.Timer(_ =>
            {
                DatabaseHelper.ExpireOverdueRentals();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(5)); // 5 dk'da bir



            var api = new ApiServer(port: 5050);
            var router = new ApiRouter(api);

            AuthRoutes.Register(router);
            RentalRoutes.Register(router);
            ScooterRoutes.Register(router);
            UserRoutes.Register(router);

            api.Start();

            // Baslangic olarak baslayacak form
            // MainForm() -> map olan form
            // LoginForm() -> login olma formu
            Application.Run(new LoginForm());
        }
    }
}