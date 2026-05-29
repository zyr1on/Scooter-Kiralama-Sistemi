using Scooter_Kiralama_Sistemi.Helpers;

namespace Scooter_Kiralama_Sistemi.API.Routes
{
    public static class UserRoutes
    {
        public static void Register(ApiRouter router)
        {
            // GET /me
            // Kullanıcının profil bilgileri (sadece okuma)
            // Dönüş: { id, name, surname, email, balance, created_at }
            router.Get("/me", ctx =>
            {
                var user = DatabaseHelper.GetUserById(ctx.UserId);

                if (user == null)
                { ctx.AsError(404, "Kullanıcı bulunamadı"); return; }

                // Hassas alanları çıkar (password_hash, role)
                ctx.AsJson(new
                {
                    id = user.id,
                    name = user.name,
                    surname = user.surname,
                    email = user.email,
                    balance = user.balance,
                    //created_at = 
                });
            }, requireAuth: true);
        }
    }
}