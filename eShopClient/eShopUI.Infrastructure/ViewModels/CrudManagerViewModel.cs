using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.ViewModels
{
    public class CrudManagerViewModel : ViewModelBase
    {
        #region Commands
        private DelegateCommand _deleteCommand;

        public DelegateCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new DelegateCommand(Delete, CanDelete, false);
                return _deleteCommand;
            }
        }

        private DelegateCommand _editCommand;

        public DelegateCommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new DelegateCommand(Edit, CanEdit, false);
                return _editCommand;
            }
        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new DelegateCommand(CancelEditing, CanCancelEditing, false);
                return _cancelCommand;
            }
        }

        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new DelegateCommand(Add, CanAdd, false);
                return _addCommand;
            }
        }

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new DelegateCommand(Save, CanSave, false);
                return _saveCommand;
            }
        }
        #endregion

        #region Private/Protected

        protected void UpdateCrudCommands()
        {
            ExecuteUIThread(() =>
            {
                EditCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
                CancelCommand.RaiseCanExecuteChanged();
                AddCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            });
        }

        protected virtual bool CanDelete()
        {
            return true;
        }

        protected virtual void Delete()
        {
        }

        protected virtual bool CanEdit()
        {
            return true;
        }

        protected virtual void Edit()
        {
        }


        protected virtual bool CanSave()
        {
            return true;
        }

        protected virtual void Save()
        {
        }


        protected virtual bool CanAdd()
        {
            return true;
        }

        protected virtual void Add()
        {
        }


        protected virtual bool CanCancelEditing()
        {
            return true;
        }

        protected virtual void CancelEditing()
        {
        }

        #endregion
    }
}
