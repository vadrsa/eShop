
using DevExpress.Mvvm;
using eShop.EntityViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities.Enums;
using DXInfrastructure.Attributes;

namespace eShop.EntityViewModels.Products
{
    public class ProductInfoViewModel : BindableBase, IIdEntityViewModel
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
        [GridColumn(Order = 2)]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }

        private int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                SetProperty(ref _CategoryID, value, nameof(CategoryID));
            }
        }

        private string _Category;
        [GridColumn]
        public string Category
        {
            get { return _Category; }
            set
            {
                SetProperty(ref _Category, value, nameof(Category));
            }
        }

        private int _BrandID;
        public int BrandID
        {
            get { return _BrandID; }
            set
            {
                SetProperty(ref _BrandID, value, nameof(BrandID));
            }
        }
        
        private string _Brand;
        [GridColumn]
        public string Brand
        {
            get { return _Brand; }
            set
            {
                SetProperty(ref _Brand, value, nameof(Brand));
            }
        }
        
        private int _Price;
        public int Price
        {
            get { return _Price; }
            set
            {
                SetProperty(ref _Price, value, nameof(Price));
            }
        }
        
        private string _ProductCode;
        [GridColumn("Product Code", 1)]
        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                SetProperty(ref _ProductCode, value, nameof(ProductCode));
            }
        }
        
        private Availability _Availability;
        [GridColumn]
        public Availability Availability
        {
            get { return _Availability; }
            set
            {
                SetProperty(ref _Availability, value, nameof(Availability));
            }
        }
        
        private string _Description;
        [GridColumn]
        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value, nameof(Description));
            }
        }

    }
}
