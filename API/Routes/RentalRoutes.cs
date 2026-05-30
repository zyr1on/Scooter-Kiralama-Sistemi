using System;
using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi.API.Routes
{
    public static class RentalRoutes
    {
        public static void Register(ApiRouter router)
        {
            // GET /me/rental
            // Kullanıcının atanmış (henüz başlamamış veya aktif) kiralama bilgisi
            // Dönüş: { active: bool, rental: { ... } }
            router.Get("/me/rental", ctx =>
            {
                var rental = DatabaseHelper.GetActiveRental(ctx.UserId);

                if (rental == null)
                { ctx.AsJson(new { active = false }); return; }

                ctx.AsJson(new { active = true, rental });
            }, requireAuth: true);


            // POST /rental/start
            // Body: { "qr_code": "..." }
            // Kullanıcıya atanmış scooter ile gelen qr eşleşiyorsa kiralama başlar
            // Dönüş: { success: true, started_at: "..." }
            router.Post("/rental/start", ctx =>
            {
                var body = ctx.BodyAs<QrRequest>();

                if (string.IsNullOrWhiteSpace(body?.qr_code))
                { ctx.AsError(400, "QR kod gerekli"); return; }

                // Kullanıcının atanmış aktif kiralması var mı?
                var rental = DatabaseHelper.GetActiveRental(ctx.UserId);

                if (rental == null)
                { ctx.AsError(404, "Size atanmış bir kiralama bulunamadı"); return; }

                // Kiralama zaten başlamış mı?
                if (rental.status == "active")
                { ctx.AsError(400, "Kiralama zaten başladı"); return; }

                // QR kodu scooter ile eşleşiyor mu?
                var scooter = DatabaseHelper.GetScooterByQr(body.qr_code);

                if (scooter == null || scooter.Id != rental.scooter_id)
                { ctx.AsError(400, "QR kod bu scooter ile eşleşmiyor"); return; }

                // Kiralama sürecini başlat
                var startedAt = DateTime.Now;
                DatabaseHelper.StartRental(rental.id, startedAt);
                DatabaseHelper.UpdateScooterStatus(scooter.Id, "rented");

                ctx.AsJson(new { success = true, started_at = startedAt });
            }, requireAuth: true);


            // POST /rental/end
            // Aktif kiralama sonlandır
            // Dönüş: { success: true, ended_at: "...", total_price: 0.0 }
            router.Post("/rental/end", ctx =>
            {
                var rental = DatabaseHelper.GetActiveRental(ctx.UserId);

                if (rental == null)
                { ctx.AsError(404, "Aktif kiralama bulunamadı"); return; }

                if (rental.status != "active")
                { ctx.AsError(400, "Kiralama henüz başlamadı"); return; }

                var endedAt = DateTime.Now;
                DatabaseHelper.EndRental(rental.id, endedAt);
                DatabaseHelper.UpdateScooterStatus(rental.scooter_id, "available");

                ctx.AsJson(new
                {
                    success = true,
                    ended_at = endedAt,
                    total_price = rental.total_price
                });
            }, requireAuth: true);
        }

        private class QrRequest
        {
            public string qr_code { get; set; }
        }
    }
}