using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usefull
{
    public class EditableObservableCollection<T> : ObservableCollection<T>, IEditableObject
        where T: IEditableObject
    {
        private bool isEditing;
        private bool isChanged;

        public EditableObservableCollection()
        {
        }

        public EditableObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public void BeginEdit()
        {
            if (isEditing == false)
            {
                isEditing = true;
                isChanged = false;
                foreach(T item in Items)
                {
                    item.BeginEdit();
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            isChanged = true;
        }

        public void EndEdit()
        {
            isEditing = false;

            foreach (T item in Items)
            {
                item.EndEdit();
            }
        }

        public void CancelEdit()
        {
            if (isEditing)
            {
                foreach (T item in Items)
                {
                    item.CancelEdit();
                }
                isEditing = false;
                isChanged = false;
            }
        }
        
    }
}
