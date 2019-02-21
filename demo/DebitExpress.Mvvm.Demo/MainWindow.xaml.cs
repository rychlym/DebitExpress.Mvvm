using System.Windows;

namespace DebitExpress.Mvvm.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //You can also use your favorite DI framework to inject the viewmodel.
            DataContext = new MainViewModel();
        }
    }
}
