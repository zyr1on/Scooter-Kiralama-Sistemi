using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Scooter_Kiralama_Sistemi.API
{
    public class ApiServer
    {
        public int Port { get; private set; }
        public bool IsRunning { get; private set; }

        private readonly HttpListener _listener = new();
        private Thread _thread;
        internal readonly Dictionary<string, Action<ApiContext>> Routes = new(StringComparer.OrdinalIgnoreCase);

        public ApiServer(int port = 5050)
        {
            Port = port;
            _listener.Prefixes.Add($"http://localhost:{Port}/");
            _listener.Prefixes.Add($"http://127.0.0.1:{Port}/");
        }

        public void Start()
        {
            if (IsRunning) return;
            _listener.Start();
            IsRunning = true;
            _thread = new Thread(Listen) { IsBackground = true, Name = "ApiServer" };
            _thread.Start();
            Console.WriteLine($"[API] http://127.0.0.1:{Port}/ çalışıyor.");
        }

        public void Stop()
        {
            if (!IsRunning) return;
            IsRunning = false;
            _listener.Stop();
        }

        private void Listen()
        {
            while (IsRunning)
            {
                try
                {
                    var httpCtx = _listener.GetContext();
                    ThreadPool.QueueUserWorkItem(_ => Handle(httpCtx));
                }
                catch (HttpListenerException) { }
                catch (Exception ex) { Console.WriteLine($"[API] Hata: {ex.Message}"); }
            }
        }

        private void Handle(HttpListenerContext httpCtx)
        {
            var req = httpCtx.Request;
            var key = $"{req.HttpMethod.ToUpper()}:{req.Url.AbsolutePath}";
            var ctx = new ApiContext(httpCtx);

            try
            {
                if (Routes.TryGetValue(key, out var handler))
                    handler(ctx);
                else
                    ctx.AsError(404, $"Endpoint bulunamadı: {key}");
            }
            catch (Exception ex)
            {
                ctx.AsError(500, "Sunucu hatası: " + ex.Message);
            }
        }
    }

    // ─────────────────────────────────────────────
    //  ApiContext
    // ─────────────────────────────────────────────
    public class ApiContext
    {
        public HttpListenerRequest Request { get; }
        public HttpListenerResponse Response { get; }

        // Token doğrulandıktan sonra set edilir
        public int UserId { get; set; }
        public int UserRole { get; set; } // 0=Admin, 1=User

        public string Body
        {
            get
            {
                using var r = new StreamReader(Request.InputStream, Request.ContentEncoding);
                return r.ReadToEnd();
            }
        }

        public T BodyAs<T>() => JsonSerializer.Deserialize<T>(Body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        public string Query(string key) => Request.QueryString[key];

        public string BearerToken()
        {
            var auth = Request.Headers["Authorization"] ?? "";
            return auth.StartsWith("Bearer ") ? auth[7..] : null;
        }

        public ApiContext(HttpListenerContext ctx)
        {
            Request = ctx.Request;
            Response = ctx.Response;
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        }

        // ── Response ──────────────────────────────

        public void AsJson(object data, int statusCode = 200)
            => Send(JsonSerializer.Serialize(data), statusCode);

        public void AsJson(DataTable table, int statusCode = 200)
        {
            var rows = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                rows.Add(dict);
            }
            AsJson(rows, statusCode);
        }

        public void AsError(int statusCode, string message)
            => Send(JsonSerializer.Serialize(new { error = true, status = statusCode, message }), statusCode);

        private void Send(string content, int statusCode)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                Response.StatusCode = statusCode;
                Response.ContentLength64 = bytes.Length;
                Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
            finally { Response.OutputStream.Close(); }
        }
    }
}