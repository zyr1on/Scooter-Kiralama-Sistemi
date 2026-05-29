using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi.API.Routes
{
    public static class AuthRoutes
    {
        public static void Register(ApiRouter router)
        {
            // POST /auth/login
            // Body: { "email": "...", "password": "..." }
            // Dönüş: { "token": "..." }
            router.Post("/auth/login", ctx =>
            {
                var body = ctx.BodyAs<LoginRequest>();

                if (string.IsNullOrWhiteSpace(body?.email) || string.IsNullOrWhiteSpace(body?.password))
                { ctx.AsError(400, "Email ve şifre gerekli"); return; }

                var user = DatabaseHelper.GetUserByEmailAndPassword(body.email, body.password);

                if (user == null)
                { ctx.AsError(401, "Hatalı email veya şifre"); return; }

                // role: 0=Admin, 1=User — adminler mobil uygulamaya giremesin istersen burada kontrol et
                var token = JwtHelper.Generate(user.id, (int)user.role);
                ctx.AsJson(new { token });
            });
        }

        private class LoginRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}