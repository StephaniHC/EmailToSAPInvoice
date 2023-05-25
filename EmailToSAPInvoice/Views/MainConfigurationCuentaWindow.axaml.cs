using Avalonia.Controls;
using EmailToSAPInvoice.ViewModels;
using System;

namespace EmailToSAPInvoice.Views
{
    public partial class MainConfigurationCuentaWindow : Window
    {
        public MainConfigurationCuentaWindow()
        {
            InitializeComponent(); 
            DataContext = new MainConfigurationCuentaWindowViewModel();
            var viewModel = DataContext as MainConfigurationCuentaWindowViewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }
        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }

    }
}
