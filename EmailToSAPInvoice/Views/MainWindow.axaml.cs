using Avalonia.Controls;
using EmailToSAPInvoice.ViewModels;

namespace EmailToSAPInvoice.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}