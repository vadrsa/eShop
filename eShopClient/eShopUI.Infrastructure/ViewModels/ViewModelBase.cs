using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Prism.Events;
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
        private List<SubscriptionToken> _tokens = new List<SubscriptionToken>();
        public List<SubscriptionToken> Tokens
        {
            get
            {
                return _tokens;
            }
        }

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
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    action();
                }));
                
            }
            else action();
        }

        public virtual void Cleanup()
        {
            _tokens.ForEach(t => t.Dispose());
        }

        public void Validate()
        {
            this.RaisePropertiesChanged("");
        }
    }
}
