using FastColoredTextBoxNS;
using SnipCS.ViewModel;
using System.Windows;

namespace SnipCS
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = ((ViewModelLocator)App.Current.FindResource("Locator")).Main;
        }

        private void FastColoredTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.Code = CodeTextBox.Text;
        }

        private void WindowsFormsHost_Loaded(object sender, RoutedEventArgs e)
        {
            CodeTextBox.Focus();
        }
    }
}