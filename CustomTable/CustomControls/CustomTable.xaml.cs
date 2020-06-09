using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace CustomTable.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomTable.xaml
    /// </summary>
    public partial class Table
    {
        public Table()
        {
            InitializeComponent();
        }

        public string ColumnWidth
        {
            get
            {
                return (string)GetValue(ColumnWidthProperty);
            }
            set
            {
                SetValue(ColumnWidthProperty, value);
            }
        }

        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.Register("ColumnWidth", typeof(string), typeof(Table));

        public string RowTitle
        {
            get
            {
                return (string)GetValue(ShowTotalsProperty);
            }
            set
            {
                SetValue(ShowTotalsProperty, value);
            }
        }

        public static readonly DependencyProperty RowTitleProperty =
            DependencyProperty.Register("RowTitle", typeof(string), typeof(Table));

        public bool ShowTotals
        {
            get
            {
                return (bool)GetValue(ShowTotalsProperty);
            }
            set
            {
                SetValue(ShowTotalsProperty, value);
            }
        }

        public static readonly DependencyProperty ShowTotalsProperty =
            DependencyProperty.Register("ShowTotals", typeof(bool), typeof(Table));

        public bool DragDropEnabled
        {
            get
            {
                return (bool)GetValue(DragDropEnabledProperty);
            }
            set
            {
                SetValue(DragDropEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty DragDropEnabledProperty =
            DependencyProperty.Register("DragDropEnabled", typeof(bool), typeof(Table));

        public IEnumerable HeadersSource
        {
            get
            {
                return (IEnumerable)GetValue(HeadersSourceProperty);
            }
            set
            {
                SetValue(HeadersSourceProperty, value);
            }
        }

        public IEnumerable RowsSource
        {
            get
            {
                return (IEnumerable)GetValue(RowsSourceProperty);
            }
            set
            {
                SetValue(RowsSourceProperty, value);
            }
        }

        public IEnumerable TotalsSource
        {
            get
            {
                return (IEnumerable)GetValue(TotalsSourceProperty);
            }
            set
            {
                SetValue(TotalsSourceProperty, value);
            }
        }

        public double GrandTotalSource
        {
            get
            {
                return (double)GetValue(GrandTotalSourceProperty);
            }
            set
            {
                SetValue(GrandTotalSourceProperty, value);
            }
        }

        public static readonly DependencyProperty HeadersSourceProperty =
            DependencyProperty.Register("HeadersSource", typeof(IEnumerable), typeof(Table));
        public static readonly DependencyProperty RowsSourceProperty =
            DependencyProperty.Register("RowsSource", typeof(IEnumerable), typeof(Table));
        public static readonly DependencyProperty TotalsSourceProperty =
            DependencyProperty.Register("TotalsSource", typeof(IEnumerable), typeof(Table));
        public static readonly DependencyProperty GrandTotalSourceProperty =
            DependencyProperty.Register("GrandTotalSource", typeof(double), typeof(Table));

        public DataTemplate ColumnHeaderTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ColumnHeaderTemplateProperty);
            }
            set
            {
                SetValue(ColumnHeaderTemplateProperty, value);
            }
        }

        public DataTemplate RowHeaderTemplate
        {
            get
            {
                return (DataTemplate)GetValue(RowHeaderTemplateProperty);
            }
            set
            {
                SetValue(RowHeaderTemplateProperty, value);
            }
        }

        public DataTemplate ValueTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ValueTemplateProperty);
            }
            set
            {
                SetValue(ValueTemplateProperty, value);
            }
        }

        public DataTemplate RowTotalTemplate
        {
            get
            {
                return (DataTemplate)GetValue(RowTotalTemplateProperty);
            }
            set
            {
                SetValue(RowTotalTemplateProperty, value);
            }
        }

        public DataTemplate TotalTemplate
        {
            get
            {
                return (DataTemplate)GetValue(TotalTemplateProperty);
            }
            set
            {
                SetValue(TotalTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty ColumnHeaderTemplateProperty =
            DependencyProperty.Register("ColumnHeaderTemplate", typeof(DataTemplate), typeof(Table));
        public static readonly DependencyProperty RowHeaderTemplateProperty =
            DependencyProperty.Register("RowHeaderTemplate", typeof(DataTemplate), typeof(Table));
        public static readonly DependencyProperty ValueTemplateProperty =
            DependencyProperty.Register("ValueTemplate", typeof(DataTemplate), typeof(Table));
        public static readonly DependencyProperty RowTotalTemplateProperty =
            DependencyProperty.Register("RowTotalTemplate", typeof(DataTemplate), typeof(Table));
        public static readonly DependencyProperty TotalTemplateProperty =
            DependencyProperty.Register("TotalTemplate", typeof(DataTemplate), typeof(Table));

        public bool ShowNewColumnButton
        {
            get
            {
                return (bool)GetValue(ShowNewColumnButtonProperty);
            }
            set
            {
                SetValue(ShowNewColumnButtonProperty, value);
            }
        }

        public static readonly DependencyProperty ShowNewColumnButtonProperty =
            DependencyProperty.Register("ShowNewColumnButton", typeof(bool), typeof(Table));

        public event RoutedEventHandler ValueChangedEvent
        {
            add
            {
                AddHandler(ValueChangedEventProperty, value);
            }
            remove
            {
                RemoveHandler(ValueChangedEventProperty, value);
            }
        }

        public event RoutedEventHandler NewColumnButtonEvent
        {
            add
            {
                AddHandler(NewColumnButtonEventProperty, value);
            }
            remove
            {
                RemoveHandler(NewColumnButtonEventProperty, value);
            }
        }

        public static readonly RoutedEvent ValueChangedEventProperty =
            EventManager.RegisterRoutedEvent("ValueChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Table));
        public static readonly RoutedEvent NewColumnButtonEventProperty =
            EventManager.RegisterRoutedEvent("NewColumnButtonEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Table));

        private void ValueTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ValueChangedEventProperty);
            RaiseEvent(newEventArgs);
        }
        private void AddNewColumnButton_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(NewColumnButtonEventProperty);
            RaiseEvent(newEventArgs);
        }
    }
}

namespace CustomTableConverters
{
    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value is bool v)
                return v ? Visibility.Visible : Visibility.Collapsed;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
