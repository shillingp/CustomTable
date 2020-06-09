using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CustomTable.Classes
{
    public class GenericBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class GenericTable<S, T, U> : GenericBase
        where U : IComparable
    {
        public ObservableCollection<Wrapper<U>> Headers { get; set; } = new ObservableCollection<Wrapper<U>>();
        public ObservableCollection<GenericTableRow<S, T>> Rows { get; set; } = new ObservableCollection<GenericTableRow<S, T>>();
        public ObservableCollection<T> Totals { get; set; } = new ObservableCollection<T>();
        public T GrandTotal { get; set; } = default(T);

        public Func<IEnumerable<GenericTableRow<S, T>>, int, T> TotalSum { get; set; } =
            (rows, colIndex) => rows.Where(row => row.IsRequired)
                .Select(row => row.Values[colIndex])
                .Aggregate(default(T), (acc, v) => Add(acc, v.Content));

        public GenericTableRow<S, T> AddRow(GenericTableRow<S, T> row) => AddRow(index: Rows.IndexOf(row));
        public GenericTableRow<S, T> AddRow(S header = default, int index = -1)
        {
            index = index != -1 ? index + 1 : Rows.Count;
            Rows.Insert(index, new GenericTableRow<S, T>
            {
                RowHeader = header == null ? new Wrapper<S>(default) : new Wrapper<S>(header),
                Values = new ObservableCollection<Wrapper<T>>(
                    Enumerable.Repeat(0, Headers.Count)
                        .Select(i => new Wrapper<T>()))
            });
            Update();
            return Rows.ElementAt(index);
        }

        public void RemoveRow(GenericTableRow<S, T> row) => Rows.Remove(row);

        public Wrapper<U> AddColumn(U header = default)
        {
            Headers.Add(header == null ? new Wrapper<U>() : new Wrapper<U>(header));
            Totals.Add(default);
            foreach (GenericTableRow<S, T> row in Rows)
                row.Values.Add(new Wrapper<T>());

            Update();
            return Headers.Last();
        }

        public void RemoveColumn(U headerItem)
        {
            int headerIndex = Headers.Select((v, i) => new { val = v, index = i }).First(obj => obj.val.Content.Equals(headerItem)).index;

            Headers.RemoveAt(headerIndex);
            Totals.RemoveAt(headerIndex);

            foreach (GenericTableRow<S, T> row in Rows)
                row.Values.RemoveAt(headerIndex);

            Update();
        }

        public void Update()
        {
            foreach (GenericTableRow<S, T> row in Rows)
                row.Total = row.Values
                    .Aggregate(default(T), (acc, v) => Add(acc, v.Content));

            Totals = new ObservableCollection<T>(
                Enumerable.Range(0, Headers.Count)
                    .Select(col => TotalSum(Rows, col)));

            GrandTotal = Totals
                .Aggregate(default(T), (acc, v) => Add(acc, v));
        }

        public static T Add(T number1, T number2)
        {
            dynamic a = number1;
            dynamic b = number2;
            return a + b;
        }
    }

    public class GenericTableRow<S, T> : GenericBase
    {
        public Wrapper<S> RowHeader { get; set; } = new Wrapper<S>(default);
        public ObservableCollection<Wrapper<T>> Values { get; set; }
            = new ObservableCollection<Wrapper<T>>();
        public T Total { get; set; } = default(T);
        public bool IsRequired { get; set; } = true;
    }

    public class Wrapper<T> : GenericBase
    {
        public T Content { get; set; } = default;

        public Wrapper() { }
        public Wrapper(T content)
        {
            Content = content;
        }
    }
}

