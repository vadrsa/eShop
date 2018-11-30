using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EntityDTO.Products
{
    public class CategoryTreeItemViewModel : BindableBase
    {


        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                SetProperty(ref _ID, value, nameof(ID));
            }
        }


        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }


        private int _ProductCount;
        public int ProductCount
        {
            get { return _ProductCount; }
            set
            {
                SetProperty(ref _ProductCount, value, nameof(ProductCount));
            }
        }


        private ObservableCollection<CategoryTreeItemViewModel> _Children;
        public ObservableCollection<CategoryTreeItemViewModel> Children
        {
            get { return _Children; }
            set
            {
                SetProperty(ref _Children, value, nameof(Children));
            }
        }

    }
}
