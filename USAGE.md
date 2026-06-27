# Steam Plugin Manager - Kullanım Rehberi

## Başlangıç

### 1. Uygulamayı Çalıştır
```
SteamPluginManager.exe
```

### 2. Plugin Dosyası Oluştur

`plugins/` klasöründe `.plugin` dosyası oluştur:

```json
{
  "name": "Oyun Adı",
  "appId": 123456,
  "executable": "game.exe",
  "launchPath": "C:\\Games\\GameFolder",
  "icon": "icon.png",
  "banner": "banner.png",
  "description": "Oyun Açıklaması",
  "developer": "Geliştirici Adı",
  "tags": ["tag1", "tag2"],
  "version": "1.0",
  "enabled": true
}
```

### 3. Oyun Ekle

1. Uygulamada Refresh butonuna tıkla
2. Oyunu listeden seç
3. "Add to Steam" butonuna tıkla
4. Başarı mesajını bekle
5. Steam'i yeniden başlat

### 4. Steam'de Oyunu Kontrol Et

Steam kütüphanesinde oyun görülecek.

## Plugin Dosya Açıklamaları

| Alan | Açıklama | Örnek |
|------|----------|-------|
| `name` | Oyunun adı | "GTA V" |
| `appId` | Steam App ID | 271590 |
| `executable` | Oyun executable dosyası | "GTA5.exe" |
| `launchPath` | Oyun ana klasörü | "C:\\Games\\GTA5" |
| `icon` | İkon dosyası | "icon.png" |
| `banner` | Banner dosyası | "banner.png" |
| `description` | Oyun açıklaması | "Grand Theft Auto V" |
| `developer` | Geliştirici | "Rockstar Games" |
| `tags` | Etiketler | ["action", "adventure"] |
| `version` | Sürüm | "1.0" |
| `enabled` | Aktif mi? | true/false |

## Sorun Giderme

### "Executable not found" hatası
- `launchPath` doğru mu kontrol et
- `executable` dosyasının adı doğru mu?
- Dosya yolu var mı?

### "Admin rights required" hatası
- Uygulamayı yönetici olarak çalıştır
- Windows'ta sağ tıkla → Yönetici olarak çalıştır

### Oyun Steam'de görünmüyor
- Steam'i yeniden başlat
- Manifest dosyasının oluşturulduğunu kontrol et:
  - `C:\Program Files (x86)\Steam\steamapps\appmanifest_[APPID].acf`

## İpuçları

- Her bir oyun için farklı `appId` kullan
- Plugin dosyalarını `.plugin` uzantısıyla kaydet
- Oyun dosya yollarını tam yol olarak belirt (D:\ vs C:\\)
- Icon ve banner dosyalarını plugin dosyasıyla aynı klasöre koy

---

Her sorunda README.md'ye göz at! 🎮
