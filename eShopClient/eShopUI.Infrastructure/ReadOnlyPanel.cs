using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eShopUI.Infrastructure
{
    public class ReadOnlyPanel
    {
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached(
                "IsReadOnly", typeof(bool), typeof(ReadOnlyPanel),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.Inherits, ReadOnlyPropertyChanged));

        public static bool GetIsReadOnly(DependencyObject o)
        {
            return (bool)o.GetValue(IsReadOnlyProperty);
        }

        public static void SetIsReadOnly(DependencyObject o, bool value)
        {
            o.SetValue(IsReadOnlyProperty, value);
        }

        private static void ReadOnlyPropertyChanged(
            DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (o is BaseEdit)
            {
                ((BaseEdit)o).IsReadOnly = (bool)e.NewValue;
            }
        }
    }
}
