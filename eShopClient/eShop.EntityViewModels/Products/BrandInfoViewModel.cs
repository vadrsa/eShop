using DevExpress.Mvvm;
using DXInfrastructure.Attributes;
using eShop.EntityViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.EntityViewModels.Products
{
    public class BrandInfoViewModel : BindableBase, IIdEntityViewModel
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
        [GridColumn(Order = 1)]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }   
        }
    }
}
