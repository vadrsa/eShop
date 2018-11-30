using Prism.Commands;
using Prism.Mvvm;
using Security.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Security.ViewModels
{
    class LoginViewModel : BindableBase
    {
        public LoginViewModel(AuthenticationController authController)
        {
            LoginCommand = new DelegateCommand<CancelEventArgs>((c) => {
                bool res = authController.Authenticate(_login, _password);
                c.Cancel = !res;
                if (!res)
                    ErrorText = "Incorrect Login and/or Password.";
                else
                    ErrorText = "";
            });
        }

        private string _login = "admin";

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value, nameof(Login)); }
        }

        private string _password = "123";

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value, nameof(Password)); }
        }


        private string _errorText = "";

        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value, nameof(ErrorText)); }
        }

        public DelegateCommand<CancelEventArgs> LoginCommand { get; set; }
    }
}
