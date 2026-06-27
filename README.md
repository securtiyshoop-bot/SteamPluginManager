# Steam Plugin Manager 🎮

Modern bir Steam kütüphane yönetim aracı. Plugin dosyalarından oyunları Steam kütüphanesine ekleyin.

## Özellikler ✨

- 🎯 **Plugin Sistemi** - `.plugin` dosyalarından oyun ekle
- 📁 **Dosya Yönetimi** - Oyun dosya yollarını yönet
- 🎨 **Modern UI** - Dark theme ile tasarlanmış
- 🚀 **Hızlı İşlem** - Tek tıkla oyun ekle/kaldır
- 📊 **Oyun Listesi** - Tüm oyunlarını bir yerde göster
- ⚙️ **Yapılandırma** - Icon ve banner desteği

## Kurulum

1. Release sayfasından `SteamPluginManager.exe` indir
2. Çalıştır
3. Plugin dosyaları ekle
4. "Steam'e Ekle" butonuna tıkla

## Plugin Dosya Formatı

```json
{
  "name": "GTA V",
  "appId": 271590,
  "executable": "GTA5.exe",
  "launchPath": "C:\\Games\\GTA5",
  "icon": "gta5_icon.png",
  "banner": "gta5_banner.png",
  "description": "Grand Theft Auto V",
  "developer": "Rockstar Games",
  "tags": ["action", "adventure"],
  "version": "1.0",
  "enabled": true
}
```

## Kullanım

1. Plugin dosyaları `plugins/` klasörüne yerleştir
2. Uygulamayı aç
3. Oyunları seç
4. "Steam'e Ekle" tıkla

## Gereksinimler

- Windows 7+
- .NET Framework 4.7+ veya .NET 6+
- Admin hakları (Steam erişimi için)

## Lisans

MIT License

## Geliştirici

securtiyshoop-bot

---

📚 Daha detaylı bilgi için [USAGE.md](USAGE.md) dosyasına bak!