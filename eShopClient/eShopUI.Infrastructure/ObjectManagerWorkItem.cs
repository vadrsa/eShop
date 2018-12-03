using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Utils.Design;
using eShopUI.Infrastructure.ViewModels;
using System.Windows;
using eShop.EntityViewModels.Interfaces;
using DXInfrastructure.Imaging;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;

namespace eShopUI.Infrastructure
{
    public abstract class ObjectManagerWorkItem<TListView, TDetailsView, TInfo, TDetail, TInfoViewModel, TDetailViewModel> : Workitem
        where TListView : FrameworkElement, new()
        where TDetailsView : FrameworkElement, new()
        where TInfoViewModel : IIdEntityViewModel, new()
        where TDetailViewModel : IIdEntityViewModel, new()
    {
        #region Private Fields

        private TListView _listView;
        private ObjectListViewModel<TInfo, TInfoViewModel> _listViewModel;
        private TDetailsView _detailsView;
        private ObjectDetailViewModel<TDetail, TDetailViewModel> _detailsViewModel;

        #endregion

        #region Constructor

        public ObjectManagerWorkItem(IContainerExtension container) : base(container)
        {
        }

        #endregion

        #region Properties

        public override void Run()
        {
            
            this._detailsView = new TDetailsView();
            ShellExtensionService.ShowInContentRegion(this._detailsView);
            this._detailsViewModel = (ObjectDetailViewModel<TDetail, TDetailViewModel>)_detailsView.DataContext;
            
            this._listView = ContainerProvider.Resolve<TListView>();
            ShellExtensionService.ShowInDockRegion(this._listView);
            this._listViewModel = (ObjectListViewModel<TInfo, TInfoViewModel>)this._listView.DataContext;

            base.Run();

        }

        public override void Terminate()
        {
            base.Terminate();
            ShellExtensionService.RemoveFromContentRegion(this._detailsView);
            ShellExtensionService.RemoveFromDockRegion(this._listView);
            _detailsViewModel.Cleanup();
            _listViewModel.Cleanup();
        }

        private ICommand CombineCommands(ICommand command1, ICommand command2)
        {
            CompositeCommand command = new CompositeCommand();
            command.RegisterCommand(command1);
            command.RegisterCommand(command2);
            return command;
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
            buttonSave.Command = CombineCommands(_detailsViewModel.SaveCommand, _listViewModel.SaveCommand);

            BarButtonItem buttonAdd = new BarButtonItem();
            buttonAdd.Content = "Add";
            buttonAdd.Glyph = AppImageHelper.GetImageSource("Add", ImageSize.Size16x16);
            buttonAdd.LargeGlyph = AppImageHelper.GetImageSource("Add", ImageSize.Size32x32);
            buttonAdd.Command = CombineCommands(_detailsViewModel.AddCommand, _listViewModel.AddCommand);

            BarButtonItem buttonEdit = new BarButtonItem();
            buttonEdit.Content = "Edit";
            buttonEdit.Glyph = AppImageHelper.GetImageSource("Edit", ImageSize.Size16x16);
            buttonEdit.LargeGlyph = AppImageHelper.GetImageSource("Edit", ImageSize.Size32x32);
            buttonEdit.Command = CombineCommands(_detailsViewModel.EditCommand, _listViewModel.EditCommand);

            BarButtonItem buttonCancel = new BarButtonItem();
            buttonCancel.Content = "Cancel";
            buttonCancel.Glyph = AppImageHelper.GetImageSource("Cancel", ImageSize.Size16x16);
            buttonCancel.LargeGlyph = AppImageHelper.GetImageSource("Cancel", ImageSize.Size32x32);
            buttonCancel.Command = CombineCommands(_detailsViewModel.CancelCommand, _listViewModel.CancelCommand);

            BarButtonItem buttonDelete = new BarButtonItem();
            buttonDelete.Content = "Delete";
            buttonDelete.Glyph = AppImageHelper.GetImageSource("Delete", ImageSize.Size16x16);
            buttonDelete.LargeGlyph = AppImageHelper.GetImageSource("Delete", ImageSize.Size32x32);
            buttonDelete.Command = CombineCommands(_detailsViewModel.DeleteCommand, _listViewModel.DeleteCommand);


            group.ItemLinks.Add(buttonAdd);
            group.ItemLinks.Add(buttonSave);
            group.ItemLinks.Add(buttonEdit);
            group.ItemLinks.Add(buttonCancel);
            group.ItemLinks.Add(buttonDelete);
            ShellExtensionService.AddCommandGroupExtension(WorkItemName, DefaultCommandPageName, group);
        }

        #endregion
    }
}
