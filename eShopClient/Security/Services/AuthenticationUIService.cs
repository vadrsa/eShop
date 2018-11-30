using EntityDTO;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.XtraEditors.Controls;
using eShopUI.Infrastructure.Interfaces;
using Modules.Security.ViewModels;
using Modules.Security.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EntityDTO.Users;

namespace Security.Services
{
    public class AuthenticationUIService : IAuthenticationUIService
    {
        public bool IsAuthenticated()
        {
            return GetCurrentUser() != null;
        }

        public UserDTO GetCurrentUser()
        {
            return (Application.Current as IApplication).User;
        }

        public void StartAuthentication()
        {
            LoginView loginView = new LoginView();
            LoginViewModel loginViewModel = (LoginViewModel)loginView.DataContext;
            UICommand loginCommand = new UICommand
            {
                Caption = "Login",
                IsCancel = false,
                IsDefault = true,
                Command = loginViewModel.LoginCommand
            };
            DXDialogWindow dialogWindow = new DXDialogWindow("Login", new List<UICommand> { loginCommand });
            dialogWindow.Width = 300;
            dialogWindow.Height = 300;
            dialogWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialogWindow.ResizeMode = System.Windows.ResizeMode.NoResize;
            dialogWindow.Content = loginView;
            dialogWindow.ShowDialogWindow();
        }
    }
}
