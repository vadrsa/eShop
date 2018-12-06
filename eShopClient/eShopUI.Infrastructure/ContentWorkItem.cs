using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm;
using System.Drawing;
using DevExpress.Xpf.Core;
using DevExpress.Utils.Design;
using System.Windows.Input;
using System.Windows;
using eShop.EntityViewModels.Interfaces;
using eShopUI.Infrastructure.ViewModels;
using DXInfrastructure.Imaging;
using System.ComponentModel;
using ModelChangeTracking;

namespace eShopUI.Infrastructure
{
    public abstract class ContentWorkItem<T, TView, TViewModel> : Workitem
        where TView : FrameworkElement, new()
        where TViewModel : IIdEntityViewModel, IEditTracking, new()
    {
        #region Private Fields

        private TView _view;
        private ContentViewModel<T, TViewModel> _viewModel;

        #endregion

        #region Constructor

        public ContentWorkItem(IContainerExtension container) : base(container)
        {
        }

        #endregion

        #region Public Methods

        public override void Run()
        {
            this._view = ContainerProvider.Resolve<TView>();
            this._viewModel = (ContentViewModel<T, TViewModel>)_view.DataContext;
            base.Run();

            ShellExtensionService.ShowInContentRegion(this._view);

        }

        protected override void AddAdditionalCommandGroups()
        {
            base.AddAdditionalCommandGroups();

            RibbonPageGroup group = new RibbonPageGroup();
            group.Caption = DefaultCommandGroupName;

            BarButtonItem buttonSave = new BarButtonItem();
            buttonSave.Content = "Save";
            buttonSave.Glyph = AppImageHelper.GetImageSource("Save", ImageSize.Size16x16);
            buttonSave.LargeGlyph = AppImageHelper.GetImageSource("Save", ImageSize.Size32x32);
            buttonSave.Command = _viewModel.SaveCommand;

            BarButtonItem buttonEdit = new BarButtonItem();
            buttonEdit.Content = "Edit";
            buttonEdit.Glyph = AppImageHelper.GetImageSource("Edit", ImageSize.Size16x16);
            buttonEdit.LargeGlyph = AppImageHelper.GetImageSource("Edit", ImageSize.Size32x32);
            buttonEdit.Command = _viewModel.EditCommand;

            BarButtonItem buttonCancel = new BarButtonItem();
            buttonCancel.Content = "Cancel";
            buttonCancel.Glyph = AppImageHelper.GetImageSource("Cancel", ImageSize.Size16x16);
            buttonCancel.LargeGlyph = AppImageHelper.GetImageSource("Cancel", ImageSize.Size32x32);
            buttonCancel.Command = _viewModel.CancelCommand;
            
            group.ItemLinks.Add(buttonSave);
            group.ItemLinks.Add(buttonEdit);
            group.ItemLinks.Add(buttonCancel);
            ShellExtensionService.AddCommandGroupExtension(WorkItemName, DefaultCommandPageName, group);
        }

        public override void Terminate()
        {
            base.Terminate();
            _viewModel.Cleanup();
            ShellExtensionService.RemoveFromContentRegion(this._view);
        }
        
        #endregion
    }
}
