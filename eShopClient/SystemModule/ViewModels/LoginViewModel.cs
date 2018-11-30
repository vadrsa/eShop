using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.System
{
    class LoginViewModel : BindableBase
    {
        public LoginViewModel()
        {

        }
        private string _login = "login";

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _password = "pass";

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
    }
}
