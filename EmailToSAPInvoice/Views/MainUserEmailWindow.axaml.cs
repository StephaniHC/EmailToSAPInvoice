using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using EmailToSAPInvoice.ViewModels;

namespace EmailToSAPInvoice.Views
{
    public partial class MainUserEmailWindow : Window
    {
        public MainUserEmailWindow()
        {
            InitializeComponent();
            DataContext = new MainUserEmailWindowViewModel();
            var viewModel = DataContext as MainUserEmailWindowViewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                MainUserEmailWindowViewModel viewModel = (MainUserEmailWindowViewModel)this.DataContext;
                string nombreProveedor = radioButton.Content.ToString();
                viewModel.Proveedor = nombreProveedor;
            }
        }
        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
