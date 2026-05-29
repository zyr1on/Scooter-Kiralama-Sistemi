# Scooter Kiralama Sistemi — API Dokümantasyonu

**Base URL:** `http://127.0.0.1:5050`  
**Format:** JSON  
**Auth:** Bearer Token (JWT)

---

## Genel Kurallar

### Authorization Header
Token gerektiren tüm isteklerde:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
```

### Hata Formatı
```json
{
  "error": true,
  "status": 401,
  "message": "Giriş yapmanız gerekiyor"
}
```

| Status | Anlam |
|--------|-------|
| 200 | Başarılı |
| 400 | Geçersiz istek / iş kuralı hatası |
| 401 | Token yok veya geçersiz |
| 403 | Yetki yok |
| 404 | Bulunamadı |
| 500 | Sunucu hatası |

---

## Endpoints

---

### 1. Login
**`POST /auth/login`** — Auth gerektirmez.

```
POST http://127.0.0.1:5050/auth/login
Content-Type: application/json

{
  "email": "kullanici@mail.com",
  "password": "sifre123"
}
```

**Başarılı `200`:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiJ9.eyJ1c2VySWQiOjEsInJvbGUiOjEsImV4cCI6MTc..."
}
```

**Hata `401`:**
```json
{
  "error": true,
  "status": 401,
  "message": "Hatalı email veya şifre"
}
```

> Token 7 gün geçerlidir.

---

### 2. Profil Bilgisi
**`GET /me`** — Auth gerektirir.

```
GET http://127.0.0.1:5050/me
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
```

**Başarılı `200`:**
```json
{
  "id": 1,
  "name": "Ahmet",
  "surname": "Yılmaz",
  "email": "ahmet@mail.com",
  "balance": 150.00,
  "created_at": "2025-01-15 10:30:00"
}
```

**Hata `401`:**
```json
{
  "error": true,
  "status": 401,
  "message": "Giriş yapmanız gerekiyor"
}
```

> `password_hash` ve `role` döndürülmez.

---

### 3. Aktif / Atanmış Kiralama
**`GET /me/rental`** — Auth gerektirir.

```
GET http://127.0.0.1:5050/me/rental
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
```

**Kiralama yoksa `200`:**
```json
{
  "active": false
}
```

**Kiralama varsa `200`:**
```json
{
  "active": true,
  "rental": {
    "id": 42,
    "user_id": 1,
    "scooter_id": 7,
    "start_date": "2025-05-20 14:00:00",
    "end_date": null,
    "days": 2,
    "total_price": 80.00,
    "status": "active",
    "created_at": "2025-05-20 13:55:00",
    "scooter_name": "Scooter-07",
    "qr_code": "QR-ABC123"
  }
}
```

**`status` değerleri:**

| Değer | Anlam |
|-------|-------|
| `pending` | Atandı, QR henüz okutulmadı |
| `active` | QR okutuldu, süre başladı |
| `finished` | Kiralama tamamlandı |

---

### 4. Kiralama Başlat (QR Okut)
**`POST /rental/start`** — Auth gerektirir.

Kullanıcıya admin tarafından atanmış `pending` durumda kiralama olmalı.  
QR kodu atanmış scooter ile eşleşirse süre o andan başlar.

```
POST http://127.0.0.1:5050/rental/start
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
Content-Type: application/json

{
  "qr_code": "QR-ABC123"
}
```

**Başarılı `200`:**
```json
{
  "success": true,
  "started_at": "2025-05-20T14:05:32Z"
}
```

**Hata `404` — Atanmış kiralama yok:**
```json
{
  "error": true,
  "status": 404,
  "message": "Size atanmış bir kiralama bulunamadı"
}
```

**Hata `400` — Kiralama zaten başladı:**
```json
{
  "error": true,
  "status": 400,
  "message": "Kiralama zaten başladı"
}
```

**Hata `400` — QR eşleşmedi:**
```json
{
  "error": true,
  "status": 400,
  "message": "QR kod bu scooter ile eşleşmiyor"
}
```

---

### 5. Kiralama Bitir
**`POST /rental/end`** — Auth gerektirir.

```
POST http://127.0.0.1:5050/rental/end
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
```

**Başarılı `200`:**
```json
{
  "success": true,
  "ended_at": "2025-05-22T09:15:00Z",
  "total_price": 80.00
}
```

**Hata `404` — Aktif kiralama yok:**
```json
{
  "error": true,
  "status": 404,
  "message": "Aktif kiralama bulunamadı"
}
```

**Hata `400` — Henüz başlamadı:**
```json
{
  "error": true,
  "status": 400,
  "message": "Kiralama henüz başlamadı"
}
```

---

### 6. GPS Güncelle
**`POST /scooter/location`** — Auth gerektirir.

Mobil uygulama tarafından her 1 dakikada bir arka planda çağrılır.

```
POST http://127.0.0.1:5050/scooter/location
Authorization: Bearer eyJhbGciOiJIUzI1NiJ9...
Content-Type: application/json

{
  "lat": 40.193489,
  "lng": 29.063541
}
```

**Başarılı `200`:**
```json
{
  "success": true
}
```

**Hata `400`:**
```json
{
  "error": true,
  "status": 400,
  "message": "Aktif kiralama yok"
}
```

---

## Kullanıcı Akışı

```
1. POST /auth/login          → token al
         ↓
2. GET  /me/rental           → atanmış scooter var mı bak (status: pending)
         ↓
3. POST /rental/start        → QR okut → süre başlar (status: active)
         ↓
4. POST /scooter/location    → her 1 dk'da bir (arka planda, status: active süresince)
         ↓
5. POST /rental/end          → kiralama bitir (status: finished)
```

---

## Notlar

- Tüm tarihler UTC, format: `yyyy-MM-dd HH:mm:ss`
- Scooter ekleme/silme, kullanıcı yönetimi, kiralama atama → admin form üzerinden, API'de yok
- Scooter listesi dönmez — kullanıcı sadece kendi atamasını görür
- `balance` sadece okunur, değiştirilemez