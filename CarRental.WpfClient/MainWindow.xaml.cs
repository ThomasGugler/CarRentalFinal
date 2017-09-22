namespace CarRental.WpfClient
{
    using System.Windows;
    using CarRental.WpfClient.ViewModel;
    using GalaSoft.MvvmLight.Ioc;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = SimpleIoc.Default.GetInstance<IMainViewModel>();
        }
    }
}
