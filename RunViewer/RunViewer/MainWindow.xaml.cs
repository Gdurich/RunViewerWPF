using RunViewer.ViewModels;
using System;
using System.Windows;

namespace RunViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new MainWindowViewModel();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                progressBar.Visibility = Visibility.Visible;
                await viewModel.LoadItems();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                progressBar.Visibility = Visibility.Hidden;
            }
        }
    }

}
