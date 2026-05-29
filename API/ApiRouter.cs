using System;

namespace Scooter_Kiralama_Sistemi.API
{
    public class ApiRouter
    {
        private readonly ApiServer _server;

        public ApiRouter(ApiServer server)
        {
            _server = server;
        }

        public ApiRouter Get(string path, Action<ApiContext> handler,
            bool requireAuth = false, bool requireAdmin = false)
            => Register("GET", path, handler, requireAuth, requireAdmin);

        public ApiRouter Post(string path, Action<ApiContext> handler,
            bool requireAuth = false, bool requireAdmin = false)
            => Register("POST", path, handler, requireAuth, requireAdmin);

        private ApiRouter Register(string method, string path, Action<ApiContext> handler,
            bool requireAuth, bool requireAdmin)
        {
            _server.Routes[$"{method}:{path}"] = ctx =>
            {
                // Auth gerekiyorsa token kontrol et
                if (requireAuth || requireAdmin)
                {
                    var token = ctx.BearerToken();
                    var result = JwtHelper.Validate(token);

                    if (result == null)
                    { ctx.AsError(401, "Giriş yapmanız gerekiyor"); return; }

                    ctx.UserId = result.Value.UserId;
                    ctx.UserRole = result.Value.Role;

                    // Admin gerektiriyorsa rol kontrol et (0 = Admin)
                    if (requireAdmin && ctx.UserRole != 0)
                    { ctx.AsError(403, "Bu işlem için yetkiniz yok"); return; }
                }

                handler(ctx);
            };

            return this;
        }
    }
}