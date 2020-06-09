using CustomTable.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CustomTable.CustomControls.Table;

namespace CustomTable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public GenericTable<string, double, string> MainTable { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;

            MainTable = new GenericTable<string, double, string>();

            MainTable.AddColumn("Col 1");
            MainTable.AddColumn("Col 2");
            MainTable.AddColumn("Col 3");

            MainTable.AddRow("Row 1");
            MainTable.AddRow("Row 2");
            MainTable.AddRow("Row 3");

            MainTable.Update();
        }

        private void UpdateTableValues(object sender, RoutedEventArgs e)
        {
            MainTable.Update();
        }
    }

    
}
