using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using eShop.EntityViewModels.Interfaces;
using SharedEntities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.EntityViewModels.Products
{
    public class ProductDetailViewModel : BindableBase, IIdEntityViewModel, IDataErrorInfoExtended
    {
        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get { return IDataErrorInfoHelper.GetErrorText(this, columnName); }
        }

        bool IDataErrorInfoExtended.HasErrors
        {
            get
            {
                return IDataErrorInfoHelper.HasErrors(this);
            }
        }

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
        [Required]
        [MinLength(3)]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value, nameof(Name));
            }
        }


        private EntityStatus _Status;
        public EntityStatus Status
        {
            get { return _Status; }
            set
            {
                SetProperty(ref _Status, value, nameof(Status));
            }
        }

        private int _CategoryID;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Category field is required.")]
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                SetProperty(ref _CategoryID, value, nameof(CategoryID));
            }
        }

        private string _Category;
        public string Category
        {
            get { return _Category; }
            set
            {
                SetProperty(ref _Category, value, nameof(Category));
            }
        }

        private int _BrandID;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Brand field is required.")]
        public int BrandID
        {
            get { return _BrandID; }
            set
            {
                SetProperty(ref _BrandID, value, nameof(BrandID));
            }
        }

        private string _Brand;
        public string Brand
        {
            get { return _Brand; }
            set
            {
                SetProperty(ref _Brand, value, nameof(Brand));
            }
        }

        private int _Price;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Price
        {
            get { return _Price; }
            set
            {
                SetProperty(ref _Price, value, nameof(Price));
            }
        }

        private string _ProductCode;
        [Required]
        public string ProductCode
        {
            get { return _ProductCode; }
            set
            {
                SetProperty(ref _ProductCode, value, nameof(ProductCode));
            }
        }

        private Availability _Availability;
        public Availability Availability
        {
            get { return _Availability; }
            set
            {
                SetProperty(ref _Availability, value, nameof(Availability));
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value, nameof(Description));
            }
        }
        
        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                SetProperty(ref _Image, value, nameof(Image));
            }
        }

        private string _ImageBig;
        public string ImageBig
        {
            get { return _ImageBig; }
            set
            {
                SetProperty(ref _ImageBig, value, nameof(ImageBig));
            }
        }

        public byte[] ImageBytes { get; set; }

        public byte[] ImageBigBytes { get; set; }
    }
}
