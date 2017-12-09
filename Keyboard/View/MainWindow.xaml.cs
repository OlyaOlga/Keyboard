namespace Keyboard.View
{
    using System.Windows;
    using Keyboard.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             var view = new MainWindowViewModel(
                    new WindowMediator
                    {
                        CreateWindow = () => new Settings()
                    })
            {
                Settings = new SettingsViewModel()
            };
            DataContext = view;
            Closing += view.OnWindowClosing;
        }
    }
}
