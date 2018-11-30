using DevExpress.Mvvm;
using DevExpress.Utils.Design;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using DXInfrastructure.Imaging;
using eShopUI.Infrastructure.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eShopUI.Infrastructure
{
    public abstract class Workitem
    {
        #region Private Fields

        private IContainerExtension _containerProvider;

        private IShellExtensionService _shellExtensionService;

        private ICurrentContextService _currentContextService;

        #endregion

        #region Commands

        private ICommand _closeCommand;
        private ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new DelegateCommand(Close);
                return _closeCommand;
            }
        }

        #endregion

        #region Properties

        public abstract string WorkItemName { get; }

        protected IContainerExtension ContainerProvider { get { return _containerProvider; } }

        protected IShellExtensionService ShellExtensionService { get { return _shellExtensionService; } }

        protected ICurrentContextService CurrentContextService { get { return _currentContextService; } }


        public virtual string DefaultCommandPageName
        {
            get
            {
                return WorkItemName;
            }
        }


        public virtual string DefaultCommandGroupName
        {
            get
            {
                return "Actions";
            }
        }

        #endregion

        #region Constructor

        public Workitem(IContainerExtension container)
        {
            _containerProvider = container;
            _shellExtensionService = container.Resolve<IShellExtensionService>();
            _currentContextService = container.Resolve<ICurrentContextService>();
        }

        #endregion

        #region Public/Protected Methods


        public virtual void Run()
        {

            DateTime start = DateTime.Now;
            AddAdditionalCommandGroups();
            AddCloseCommandGroups();
            TimeSpan took = DateTime.Now - start;
            Debug.WriteLine("Command Add took: "+took.TotalSeconds);
        }


        protected virtual void Close()
        {
            CurrentContextService.CloseCurrentWorkItem();
        }


        public virtual void Terminate()
        {
            ShellExtensionService.RemoveCommandCategory(WorkItemName);
        }


        protected virtual void AddAdditionalCommandGroups()
        {
        }

        protected virtual void AddCloseCommandGroups()
        {

            RibbonPageGroup group1 = new RibbonPageGroup();
            group1.Caption = "Close";
            BarButtonItem closeButton = new BarButtonItem();
            closeButton.Content = "Close";
            closeButton.Glyph = AppImageHelper.GetImageSource("Close", ImageSize.Size16x16);
            closeButton.LargeGlyph = AppImageHelper.GetImageSource("Close", ImageSize.Size32x32);
            closeButton.Command = CloseCommand;
            group1.ItemLinks.Add(closeButton);

            ShellExtensionService.AddCommandGroupExtension(WorkItemName, DefaultCommandPageName, group1);
        }


        #endregion

    }
}
