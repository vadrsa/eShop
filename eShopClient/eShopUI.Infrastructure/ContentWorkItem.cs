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

namespace eShopUI.Infrastructure
{
    public abstract class ContentWorkItem<TView, TViewModel> : Workitem
    {
        #region Private Fields

        private TView _view;

        #endregion

        #region Constructor

        public ContentWorkItem(IContainerExtension container) : base(container)
        {
        }

        #endregion

        #region Public Methods

        public override void Run()
        {
            base.Run();
            this._view = ContainerProvider.Resolve<TView>();
            ShellExtensionService.ShowInContentRegion(this._view);
        }

        public override void Terminate()
        {
            base.Terminate();
            ShellExtensionService.RemoveFromContentRegion(this._view);
        }
        
        #endregion
    }
}
