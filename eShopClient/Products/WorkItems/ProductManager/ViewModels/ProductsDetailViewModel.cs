using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DXInfrastructure.Imaging;
using EntityDTO.Products;
using eShop.EntityViewModels;
using eShop.EntityViewModels.Interfaces;
using eShop.EntityViewModels.Products;
using eShopUI.Infrastructure.Api;
using eShopUI.Infrastructure.Attributes;
using eShopUI.Infrastructure.ViewModels;
using Modules.Products.Services;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Products.WorkItems.ProductManager.ViewModels
{
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class ProductsDetailViewModel : ObjectDetailViewModel<ProductDetailDTO, ProductDetailViewModel>, IDataErrorInfoExtended
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
        #region Private Fields
        private byte[] _oldImage;
        private CategoryService _categoryService;
        private BrandInfoService _brandService;
        #endregion

        public ProductsDetailViewModel(IContainerExtension container) : base(container)
        {
            _categoryService = new CategoryService();
            _brandService = new BrandInfoService();
            LoadCategories();
            LoadBrands();
        }

        #region Bindable Properties

        private ObservableCollection<BrandInfoViewModel> _brands;
        public ObservableCollection<BrandInfoViewModel> Brands
        {
            get { return _brands; }
            set
            {
                SetProperty(ref _brands, value, nameof(Brands));
            }
        }

        private ObservableCollection<CategoryViewModel> _categories;
        public ObservableCollection<CategoryViewModel> Categories
        {
            get { return _categories; }
            set
            {
                SetProperty(ref _categories, value, nameof(Categories));
            }
        }

        private bool hasObjectImageChanged;
        private byte[] _ObjectImage;
        [RequiredCollection]
        public byte[] ObjectImage
        {
            get { return _ObjectImage; }
            set
            {   if (value != _ObjectImage)
                    hasObjectImageChanged = true;
                SetProperty(ref _ObjectImage, value, nameof(ObjectImage));
            }
        }

        #endregion

        #region Private/Protected Methods
        
        private void LoadBrands()
        {
            Observable.FromAsync(_brandService.Get)
            .Subscribe((l) => Brands = Mapper.Map<ObservableCollection<BrandInfoViewModel>>(l),
            (e) => {
            });

        }

        private void LoadCategories()
        {
            Observable.FromAsync(_categoryService.Get)
            .Subscribe((l) => Categories = Mapper.Map<ObservableCollection<CategoryViewModel>>(l),
            (e) => {
            });
        }

        protected override void OnSave()
        {
            base.OnSave();

        }

        protected override bool IsValid()
        {
            return base.IsValid() && ObjectImage != null;
        }

        protected override ProductDetailViewModel BeforeObjectLoad(ProductDetailViewModel obj, CancellationToken token = new CancellationToken())
        {
            ProductDetailViewModel res = base.BeforeObjectLoad(obj);
            IObservable<byte[]> imageObs = Observable.FromAsync(()=> ApiImageHelper.GetImageBytesAsync(obj.Image, token));
            imageObs.Subscribe((img)=> {
                ObjectImage = img;
                hasObjectImageChanged = false;
            }, (e) => {
                ShowMessageBox("Error occured while fetching data.", "Error", System.Windows.MessageBoxButton.OK);
            }, token);
            return res;
        }

        protected override void Edit()
        {
            _oldImage = ObjectImage;
            base.Edit();
        }

        protected override void CancelEditing()
        {
            ObjectImage = _oldImage;
            base.CancelEditing();
        }

        protected override void Add()
        {
            _oldImage = ObjectImage;
            ObjectImage = null;
            base.Add();
        }

        protected override void Save()
        {
            if(IsValid() && hasObjectImageChanged)
                Object.ImageBytes = ObjectImage;
            base.Save();
        }

        #endregion
    }
}
