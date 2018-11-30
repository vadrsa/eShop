using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DXInfrastructure.DependencyProperties
{
    public class ValidationProperties
    {
        static DataLayoutControl control;
        public static readonly DependencyProperty AutoValidationProperty =
        DependencyProperty.RegisterAttached("AutoValidation",
                                            typeof(bool),
                                            typeof(ValidationProperties),
                                            new UIPropertyMetadata(false, AutoValidationPropertyChanged));
        private static void AutoValidationPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            control = (DataLayoutControl)source;
            control.IsVisibleChanged += Control_IsVisibleChanged;
            var a = control.GetBindingExpression(DataLayoutControl.CurrentItemProperty);
        }

        private static void Control_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var a = FindVisualChildren<BaseEdit>(control);
            foreach (BaseEdit control in FindLogicalChildren<BaseEdit>(control))
            {
                control.ValidateOnTextInput = true;
                control.Validate += Control_Validate;
            }
        }

        private static void Control_Validate(object sender, ValidationEventArgs e)
        {
            //var a = (sender as TextEdit).GetBindingExpression(TextEdit.TextProperty);
        }

        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                foreach (object rawChild in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (rawChild is DependencyObject)
                    {
                        DependencyObject child = (DependencyObject)rawChild;
                        if (child is T)
                        {
                            yield return (T)child;
                        }

                        foreach (T childOfChild in FindLogicalChildren<T>(child))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

        private static List<T> FindVisualChildren<T>(DependencyObject source) where T : DependencyObject
        {
            List<T> res = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(source); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(source, i);
                if (child != null && child is T)
                {
                    res.Add((T)child);
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    res.Add(childOfChild);
                }
            }
            return res;
        }

        public static void SetAutoValidation(DependencyObject element, bool value)
        {
            element.SetValue(AutoValidationProperty, value);
        }
        public static bool GetAutoValidation(DependencyObject element)
        {
            return (bool)element.GetValue(AutoValidationProperty);
        }
    }
}
