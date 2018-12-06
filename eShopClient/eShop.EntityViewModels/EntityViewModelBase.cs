using DevExpress.Mvvm;
using ModelChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.EntityViewModels
{
    [Serializable]
    public class EntityViewModelBase<T> : BindableBase, IEditTracking, IEditableObject
        where T: class, INotifyPropertyChanged
    {
        public EntityViewModelBase()
        {
            editableAdapter = new EditableAdapter<T>(this as T);
        }

        private EditableAdapter<T> editableAdapter;

        public EEditState State { get => editableAdapter.State; set => editableAdapter.State = value; }

        public bool IsChanged => editableAdapter.IsChanged;

        public bool Track { get => editableAdapter.Track; set => editableAdapter.Track = value; }

        public void AcceptChanges()
        {
            editableAdapter.AcceptChanges();
        }

        public void BeginEdit()
        {
            editableAdapter.BeginEdit();
        }

        public void CancelEdit()
        {
            editableAdapter.CancelEdit();
        }

        public void EndEdit()
        {

        }
    }
}
