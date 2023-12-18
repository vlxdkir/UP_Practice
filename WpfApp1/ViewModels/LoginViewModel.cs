using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Repos;
using System.Windows;
using System.Net.Mail;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        public LoginWindow loginWindow;

        private IUserRepository userRepository;

        //Properties
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; private set; }
        public ICommand RecoverPasswordCommand { get; private set; }
        public ICommand ShowPasswordCommand { get; private set; }
        public ICommand RememberPasswordCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanRegisterCommand);
            
        }




        private bool CanExecuteLoginCommand(object obj)
        {

            return true;
            //bool validData;
            //if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
            //    Password == null || Password.Length < 3)
            //    validData = false;
            //else
            //    validData = true;
            //return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
                Main window = new Main();
                window.Show();
                LoginWindow loginWindow1 = loginWindow;
                loginWindow1.Close();
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private void ExecuteRegisterCommand(object parameter)
        {
            userRepository.CreateUser(Username, Password);
            MessageBox.Show("Пользователь создан!");
               
        }

        private bool CanRegisterCommand(object parameter)
        {
            return true;
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
