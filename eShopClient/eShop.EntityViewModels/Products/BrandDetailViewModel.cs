using DevExpress.Mvvm;
using eShop.EntityViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.EntityViewModels.Products
{
    public class BrandDetailViewModel : BindableBase, IIdEntityViewModel
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

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                SetProperty(ref _Image, value, nameof(Image));
            }
        }

        private byte[] _ImageBytes;
        public byte[] ImageBytes
        {
            get { return _ImageBytes; }
            set
            {
                SetProperty(ref _ImageBytes, value, nameof(ImageBytes));
            }
        }
    }
}
