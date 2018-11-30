using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eShopUI.Infrastructure.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected void ShowMessageBox(string message, string caption, System.Windows.MessageBoxButton buttons = System.Windows.MessageBoxButton.OK)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() => {
                    DXMessageBox.Show(message, caption, buttons);

                });
                return;
            }
            else
            {
                DXMessageBox.Show(message, caption, buttons);
            }
        }

        protected void ExecuteUIThread(Action action)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    action();
                });
            }
            else action();
        }
    }
}
