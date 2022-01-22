using FO.UI;
using System.Windows;

namespace FO.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MainViewModel _MainViewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this._MainViewModel = mainViewModel;            
            DataContext = _MainViewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _MainViewModel.LoadAsync();
        }
    }
}
