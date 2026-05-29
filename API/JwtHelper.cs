using System;
using System.Text;
using System.Text.Json;

namespace Scooter_Kiralama_Sistemi.API
{
    /// <summary>
    /// Basit imzalı token sistemi.
    /// Gerçek JWT kütüphanesi kullanmak istersen: NuGet → System.IdentityModel.Tokens.Jwt
    /// Ama bu proje için bu yeterli.
    /// 
    /// Token formatı (Base64): header.payload.signature
    /// </summary>
    public static class JwtHelper
    {
        // Bunu appsettings / config'den oku production'da
        private const string Secret = "ScooterGizliAnahtar_Degistir_2025!";

        public static string Generate(int userId, int role)
        {
            var payload = JsonSerializer.Serialize(new
            {
                userId,
                role,
                exp = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds()
            });

            var header = B64("""{"alg":"HS256"}""");
            var body = B64(payload);
            var sig = Sign($"{header}.{body}");

            return $"{header}.{body}.{sig}";
        }

        /// <summary>Token geçerliyse (userId, role) döner. Geçersizse null.</summary>
        public static (int UserId, int Role)? Validate(string token)
        {
            try
            {
                var parts = token?.Split('.');
                if (parts == null || parts.Length != 3) return null;

                // İmza kontrolü
                var expectedSig = Sign($"{parts[0]}.{parts[1]}");
                if (expectedSig != parts[2]) return null;

                var payload = JsonSerializer.Deserialize<TokenPayload>(
                    Encoding.UTF8.GetString(Convert.FromBase64String(Pad(parts[1]))));

                // Süre kontrolü
                if (payload.exp < DateTimeOffset.UtcNow.ToUnixTimeSeconds()) return null;

                return (payload.userId, payload.role);
            }
            catch { return null; }
        }

        // ── Yardımcılar ──────────────────────────

        private static string B64(string input)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(input))
                      .TrimEnd('=').Replace('+', '-').Replace('/', '_');

        private static string Sign(string data)
        {
            var key = Encoding.UTF8.GetBytes(Secret);
            var bytes = Encoding.UTF8.GetBytes(data);
            using var hmac = new System.Security.Cryptography.HMACSHA256(key);
            return B64(Convert.ToBase64String(hmac.ComputeHash(bytes)));
        }

        private static string Pad(string b64)
        {
            b64 = b64.Replace('-', '+').Replace('_', '/');

            switch (b64.Length % 4)
            {
                case 2:
                    return b64 + "==";
                case 3:
                    return b64 + "=";
                default:
                    return b64;
            }
        }

        private class TokenPayload
        {
            public int userId { get; set; }
            public int role { get; set; }
            public long exp { get; set; }
        }
    }
}