using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using eShopUI.Infrastructure.Interfaces;
using DevExpress.Mvvm;
using System.Windows.Input;
using Prism.Events;
using eShopUI.Infrastructure.Events;

namespace eShopUI.Infrastructure
{
    public abstract class Module : IModule
    {
        #region Private Fields

        private IAuthenticationUIService _authenticationService;

        private IShellExtensionService _shellExtensionService;

        private IContainerProvider _container;

        private ICurrentContextService _currentContextService;

        private IEventAggregator _eventAggregator;

        private List<DelegateCommand> secureCommands;

        #endregion

        #region Public and Protected Methods
        public virtual void OnInitialized(IContainerProvider containerProvider)
        {
            secureCommands = new List<DelegateCommand>();

            this._shellExtensionService = containerProvider.Resolve<IShellExtensionService>();
            this._currentContextService = containerProvider.Resolve<ICurrentContextService>();
            this._container = containerProvider;
            this._authenticationService = _container.Resolve<IAuthenticationUIService>();
            this._eventAggregator = _container.Resolve<IEventAggregator>();
            this._eventAggregator.GetEvent<AuthenticatedEvent>().Subscribe(HandleUserAuthentication);
        }

        public virtual void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected ICommand ToCommand(Action method)
        {
            return new DelegateCommand(method);
        }


        protected ICommand ToSecureCommand(Action method)
        {
            DelegateCommand command = new DelegateCommand(method, SecureCanExecute);
            secureCommands.Add(command);
            return command;
        }
        
        #endregion

        #region Properties
        public abstract string Name { get; }

        public IShellExtensionService ShellExtensionService { get { return _shellExtensionService; } }

        public IContainerProvider Container { get { return _container; } }

        public IEventAggregator EventAggregator { get { return _eventAggregator; } }
        
        protected ICurrentContextService CurrentContextService { get { return _currentContextService; } }

        #endregion

        #region Private Methods
        
        private bool SecureCanExecute()
        {
            return _authenticationService.IsAuthenticated();
        }
        
        private void HandleUserAuthentication()
        {
            this.secureCommands.ForEach(c => c.RaiseCanExecuteChanged());
        }

        #endregion
    }
}
