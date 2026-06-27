using System.Windows;
using System.Collections.ObjectModel;
using SteamPluginManager.Models;
using SteamPluginManager.Services;

namespace SteamPluginManager
{
    public partial class MainWindow : Window
    {
        private PluginManager _manager;
        private ObservableCollection<GamePlugin> _plugins;

        public MainWindow()
        {
            InitializeComponent();
            _manager = new PluginManager();
            _plugins = new ObservableCollection<GamePlugin>();
            PluginGrid.ItemsSource = _plugins;
            RefreshPlugins();
        }

        private void RefreshPlugins()
        {
            _plugins.Clear();
            var plugins = _manager.LoadAllPlugins();
            foreach (var plugin in plugins)
            {
                _plugins.Add(plugin);
            }
            UpdateStatus($"✓ {plugins.Count} plugin(s) loaded");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PluginGrid.SelectedItem is GamePlugin plugin)
            {
                if (_manager.AddGameToSteam(plugin))
                {
                    UpdateStatus($"✓ '{plugin.Name}' Steam'e eklendi!");
                    MessageBox.Show($"✓ {plugin.Name} successfully added to Steam!",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdateStatus($"✗ '{plugin.Name}' eklenirken hata oluştu!");
                    MessageBox.Show($"✗ Error adding {plugin.Name} to Steam!\n\nExecutable bulunamadı veya oyun etkin değil.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir oyun seçin!", "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PluginGrid.SelectedItem is GamePlugin plugin)
            {
                if (_manager.RemoveGameFromSteam(plugin))
                {
                    UpdateStatus($"✓ '{plugin.Name}' Steam'den kaldırıldı!");
                    MessageBox.Show($"✓ {plugin.Name} removed from Steam!",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdateStatus($"✗ '{plugin.Name}' kaldırılırken hata oluştu!");
                    MessageBox.Show($"✗ Error removing {plugin.Name}!",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshPlugins();
        }

        private void UpdateStatus(string message)
        {
            StatusText.Text = message;
        }
    }
}
