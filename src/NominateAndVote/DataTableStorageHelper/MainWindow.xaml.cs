using System;
using System.Windows;

namespace DataTableStorageHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Guid a = Guid.NewGuid();
            Guid b = Guid.Parse(a.ToString());

            MessageBox.Show(object.ReferenceEquals(a, b) ? "OK " : "NOK");
            MessageBox.Show(a == b ? "OK " : "NOK");
            MessageBox.Show(a.Equals(b) ? "OK " : "NOK");
        }
    }
}