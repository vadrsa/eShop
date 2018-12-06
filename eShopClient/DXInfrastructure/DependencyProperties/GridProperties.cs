using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DXInfrastructure.Attributes;
using System.Collections.Generic;
using DevExpress.Data;

namespace DXInfrastructure.DependencyProperties
{
    public class GridProperties
    {
        public static readonly DependencyProperty GenerateColumnsProperty =
        DependencyProperty.RegisterAttached("GenerateColumns",
                                            typeof(bool),
                                            typeof(GridProperties),
                                            new UIPropertyMetadata(false, GenerateColumnsPropertyChanged));
        private static void GenerateColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (((bool)e.NewValue))
            {
                GridControl grid = (GridControl)source;

                grid.ItemsSourceChanged += (o, h) => { GenerateColumns(o as GridControl); };
            }
        }

        private static void GenerateColumns(GridControl grid)
        {
            ICollection list = grid.ItemsSource as ICollection;
            if (list == null || list.Count == 0) return;
            grid.Columns.Clear();
            IEnumerator enumarator = list.GetEnumerator();
            enumarator.MoveNext();
            Type listItemType = enumarator.Current.GetType();
            PropertyInfo[] properties = listItemType.GetProperties();
            List<GridColumnAttribute> columns = new List<GridColumnAttribute>();

            foreach (PropertyInfo propertyInfo in properties)
            {
                GridColumnAttribute gridColumnAttribute = propertyInfo.GetCustomAttribute<GridColumnAttribute>();
                if (gridColumnAttribute != null)
                {
                    gridColumnAttribute.Header = gridColumnAttribute.Header ?? propertyInfo.Name;
                    gridColumnAttribute.FieldName = gridColumnAttribute.FieldName ?? propertyInfo.Name;
                    columns.Add(gridColumnAttribute);
                }
            }
            columns.Sort((o, t) => {
                if (o.Order == 0 && t.Order == 0) return 0;
                if (o.Order == 0) return 1;
                if (t.Order == 0) return -1;
                return o.Order.CompareTo(t.Order);

            });
            foreach (GridColumnAttribute columnAttribute in columns)
            {
                GridColumn column = new GridColumn();
                column.Header = columnAttribute.Header;
                column.FieldName = columnAttribute.FieldName;
                column.Name = columnAttribute.FieldName;
                grid.Columns.Add(column);
            }

        }
        public static void SetGenerateColumns(DependencyObject element, bool value)
        {
            element.SetValue(GenerateColumnsProperty, value);
        }
        public static bool GetGenerateColumns(DependencyObject element)
        {
            return (bool)element.GetValue(GenerateColumnsProperty);
        }
    }
}
