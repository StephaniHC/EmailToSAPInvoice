using Avalonia.Controls;
using EmailToSAPInvoice.ViewModels;
using System; 

namespace EmailToSAPInvoice.Views
{
    public partial class MainConfigurationPerfilWindow : Window
    {
        public MainConfigurationPerfilWindow()
        {
            InitializeComponent();
            DataContext = new MainConfigurationPerfilWindowViewModel();
            var viewModel = DataContext as MainConfigurationPerfilWindowViewModel; 
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }
        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
