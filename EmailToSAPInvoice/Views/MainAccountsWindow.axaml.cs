using Avalonia.Controls;
using Avalonia.Interactivity;
using EmailToSAPInvoice.ViewModels;
using System;

namespace EmailToSAPInvoice.Views
{
    public partial class MainAccountsWindow : Window
    {
        public MainAccountsWindow()
        {
            InitializeComponent();
            DataContext = new MainAccountsWindowViewModel();
            var viewModel = DataContext as MainAccountsWindowViewModel;
            viewModel.CloseWindow += ViewModel_CloseWindow;
        }
        private void ViewModel_CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                MainAccountsWindowViewModel viewModel = (MainAccountsWindowViewModel)this.DataContext;
                string nombreMetodo = radioButton.Content.ToString();
                if (nombreMetodo == "SI")  viewModel.Parther = true; 
                else  viewModel.Parther = false; 
            }
        }
    }
}
