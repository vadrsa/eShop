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
        public static readonly DependencyProperty ItemsListProperty =
        DependencyProperty.RegisterAttached("ItemsList",
                                            typeof(ICollection),
                                            typeof(GridProperties),
                                            new UIPropertyMetadata(null, ItemsListPropertyChanged));
        private static void ItemsListPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            GridControl grid = (GridControl)source;
            ICollection list = (ICollection)e.NewValue;
            if (list == null || list.Count == 0) return;
            IEnumerator enumarator = list.GetEnumerator();
            enumarator.MoveNext();
            Type listItemType = enumarator.Current.GetType();
            PropertyInfo[] properties = listItemType.GetProperties();
            List<GridColumnAttribute> columns = new List<GridColumnAttribute>();

            foreach (PropertyInfo propertyInfo in properties)
            {
                GridColumnAttribute gridColumnAttribute = propertyInfo.GetCustomAttribute<GridColumnAttribute>();
                if(gridColumnAttribute != null)
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
            foreach(GridColumnAttribute columnAttribute in columns)
            {
                GridColumn column = new GridColumn();
                column.Header = columnAttribute.Header;
                column.FieldName = columnAttribute.FieldName;
                column.Name = columnAttribute.FieldName;
                grid.Columns.Add(column);
            }
            grid.ItemsSource = list;
            
        }
        public static void SetItemsList(DependencyObject element, ICollection value)
        {
            element.SetValue(ItemsListProperty, value);
        }
        public static ICollection GetItemsList(DependencyObject element)
        {
            return (ICollection)element.GetValue(ItemsListProperty);
        }
    }
}
