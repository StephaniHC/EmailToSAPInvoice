using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using EmailToSAPInvoice.ViewModels;

namespace EmailToSAPInvoice.Views
{
    public partial class MainProviderWindow : Window
    {
        public MainProviderWindow()
        {
            InitializeComponent();
            DataContext = new MainProviderWindowViewModel();
            var viewModel = DataContext as MainProviderWindowViewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                MainProviderWindowViewModel viewModel = (MainProviderWindowViewModel)this.DataContext;
                string nombreMetodo = radioButton.Content.ToString();
                viewModel.NombreMetodo = nombreMetodo.ToLower();
            }
        }
        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
    }
}
