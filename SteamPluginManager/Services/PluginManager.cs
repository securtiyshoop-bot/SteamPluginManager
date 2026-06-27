using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SteamPluginManager.Models;
using Microsoft.Win32;

namespace SteamPluginManager.Services
{
    public class PluginManager
    {
        private readonly string _pluginsDir = "plugins";
        private string _steamDir;

        public PluginManager()
        {
            if (!Directory.Exists(_pluginsDir))
                Directory.CreateDirectory(_pluginsDir);

            _steamDir = GetSteamDirectory();
        }

        // Steam dizinini Registry'den bul
        private string GetSteamDirectory()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam"))
                {
                    if (key != null)
                    {
                        object steamPath = key.GetValue("SteamPath");
                        if (steamPath != null)
                            return steamPath.ToString();
                    }
                }
            }
            catch { }

            return @"C:\Program Files (x86)\Steam";
        }

        // Tüm plugin'leri yükle
        public List<GamePlugin> LoadAllPlugins()
        {
            var plugins = new List<GamePlugin>();

            if (!Directory.Exists(_pluginsDir))
                return plugins;

            foreach (var file in Directory.GetFiles(_pluginsDir, "*.plugin"))
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var plugin = JsonConvert.DeserializeObject<GamePlugin>(json);
                    if (plugin != null)
                        plugins.Add(plugin);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Plugin yükleme hatası {file}: {ex.Message}");
                }
            }

            return plugins.OrderBy(p => p.Name).ToList();
        }

        // Oyunu Steam'e ekle
        public bool AddGameToSteam(GamePlugin plugin)
        {
            try
            {
                if (!plugin.Enabled)
                    return false;

                string exePath = Path.Combine(plugin.LaunchPath, plugin.Executable);
                if (!File.Exists(exePath))
                    return false;

                // Manifest oluştur ve yaz
                string manifestContent = CreateManifest(plugin);
                string manifestDir = Path.Combine(_steamDir, "steamapps");
                string manifestFile = Path.Combine(manifestDir, $"appmanifest_{plugin.AppId}.acf");

                Directory.CreateDirectory(manifestDir);
                File.WriteAllText(manifestFile, manifestContent);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Steam ekleme hatası: {ex.Message}");
                return false;
            }
        }

        // Oyunu Steam'den kaldır
        public bool RemoveGameFromSteam(GamePlugin plugin)
        {
            try
            {
                string manifestFile = Path.Combine(_steamDir, "steamapps", 
                    $"appmanifest_{plugin.AppId}.acf");

                if (File.Exists(manifestFile))
                {
                    File.Delete(manifestFile);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kaldırma hatası: {ex.Message}");
                return false;
            }
        }

        // Manifest dosyası oluştur (VDF formatı)
        private string CreateManifest(GamePlugin plugin)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return $@"\"AppState\"
{{
    \"appid\"\t\t\"{plugin.AppId}\"
    \"Universe\"\t\t\"1\"
    \"name\"\t\t\"{plugin.Name}\"
    \"StateFlags\"\t\t\"4\"
    \"LastUpdated\"\t\t\"{timestamp}\"
    \"ContentUpdateNumber\"\t\t\"1\"
    \"StagingContentID\"\t\t\"0\"
    \"SteamID\"\t\t\"0\"
    \"MountedContentID\"\t\t\"0\"
    \"SymlinkContentID\"\t\t\"0\"
    \"AutoUpdateBehavior\"\t\t\"0\"
    \"AllowOtherDownloadsWhileRunning\"\t\t\"0\"
    \"InstalledDepots\"
    {{
        \"{plugin.AppId * 2}\"
        {{
            \"manifest\"\t\t\"0\"
            \"size\"\t\t\"0\"
        }}
    }}
    \"SharedContentID\"\t\t\"0\"
}}";
        }

        // Plugin oluştur
        public void CreatePlugin(GamePlugin plugin, string filename)
        {
            try
            {
                string json = JsonConvert.SerializeObject(plugin, Formatting.Indented);
                string filePath = Path.Combine(_pluginsDir, $"{filename}.plugin");
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Plugin oluşturma hatası: {ex.Message}");
            }
        }

        public string GetSteamDirectoryPath() => _steamDir;
    }
}
