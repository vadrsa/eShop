using eShopUI.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace eShopUI.ViewModels
{
    class ShellViewModel : BindableBase
    {

        #region Commands

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                    _exitCommand = new DelegateCommand(Exit);
                return _exitCommand;
            }
        }

        #endregion

        #region Public Methods

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
