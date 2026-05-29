using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi.API.Routes
{
    public static class ScooterRoutes
    {
        public static void Register(ApiRouter router)
        {
            // POST /scooter/location
            // Mobil uygulama 1 dk'da bir çağırır
            // Body: { "lat": 40.1234, "lng": 29.1234 }
            router.Post("/scooter/location", ctx =>
            {
                var body = ctx.BodyAs<LocationRequest>();

                var rental = DatabaseHelper.getActiveRental(ctx.UserId);

                if (rental == null || rental.status != "active")
                { ctx.AsError(400, "Aktif kiralama yok"); return; }

                DatabaseHelper.UpdateScooterLocation(rental.scooter_id, body.lat, body.lng);

                ctx.AsJson(new { success = true });
            }, requireAuth: true);
        }

        private class LocationRequest
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }
    }
}