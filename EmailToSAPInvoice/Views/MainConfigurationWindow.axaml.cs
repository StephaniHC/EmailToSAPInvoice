using Avalonia.Controls;
using EmailToSAPInvoice.ViewModels;

namespace EmailToSAPInvoice.Views
{
    public partial class MainConfigurationWindow : Window
    {
        public MainConfigurationWindow()
        {
            InitializeComponent();
            DataContext = new MainConfigurationWindowViewModel();
            var viewModel = DataContext as MainConfigurationWindowViewModel; 
        }
    }
}
