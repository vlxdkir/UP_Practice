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
using System.Security.Cryptography;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        public UserRepository dbLog = new UserRepository();
        private string _username;
        private string _password;
        private int _id;
        private string _errorMessage;
        private string _email;
        private bool _isViewVisible = true;
        private ObservableCollection<UserModel> users;
        private string _firstregpassword;
        private string _secondregpassword;
        public static LoginViewModel loginViewModel;
        public LoginWindow loginWindow = LoginWindow.loginWindow;
        private UserModel CurrentUser;




        private IUserRepository userRepository;
        private int currentAccessLevel;
        private Visibility _stackPanelVisibility = Visibility.Hidden;
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

        

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        //Для регистрации
        public string FirstRegPassword
        {
            get { return _firstregpassword; }
            set
            {
                if (_firstregpassword != value)
                {
                    _firstregpassword = value;
                    OnPropertyChanged(nameof(FirstRegPassword));
                }
            }
        }

        public int CurrentAccessLevel
        {
            get { return currentAccessLevel; }
            set
            {
                currentAccessLevel = value;
                VisibilityEdit();
                OnPropertyChanged("CurrentAccessLevel");
            }
        }

        private void VisibilityEdit()
        {
            if (CurrentAccessLevel == 1)
            {
                StackPanelVisibility = Visibility.Visible;
            }
        }

        public Visibility StackPanelVisibility
        {
            get { return _stackPanelVisibility; }
            set
            {
                _stackPanelVisibility = value;
                OnPropertyChanged(nameof(StackPanelVisibility));
            }
        }

        public string SecondRegPassword
        {
            get { return _secondregpassword; }
            set
            {
                if (_secondregpassword != value)
                {
                    _secondregpassword = value;
                    OnPropertyChanged(nameof(SecondRegPassword));
                }
            }
        }

        

        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }


        //-> Commands
        public ICommand LoginCommand { get; private set; }
        public ICommand RecoverPasswordCommand { get; private set; }
        public ICommand ShowPasswordCommand { get; private set; }
        public ICommand RememberPasswordCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

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

        private void OpenWindowBasedOnRole(int role)
        {
            if (role == 1)
            {
                Main adminWindow = new Main();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = adminWindow;
                adminWindow.Show();
            }
            else if (role == 0)
            {
                UserWindow userWindow = new UserWindow();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = userWindow;
                userWindow.Show();
            }
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                CurrentUser = dbLog.GetByUsername(Username);

                // Получите значение AccessLevel из объекта CurrentUser
                int role = CurrentUser?.AccessLevel ?? 0; // Здесь 0 - это значение по умолчанию, вы можете его изменить на подходящее

                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), new[] { role.ToString() });

                IsViewVisible = false;
                OpenWindowBasedOnRole(role);

                if (Application.Current.MainWindow is LoginWindow loginWindow1)
                {
                    loginWindow1.Close();
                }
            }
            else
            {
                string message = "Неверный пароль или логин.";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
        }


        private void ExecuteRegisterCommand(object parameter)
        {
            if (FirstRegPassword == SecondRegPassword)
            {
                userRepository.CreateUser(Username, FirstRegPassword, 0);
                string message = "Пользователь создан";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
            else
            {
                string message = "Пользователь не создан. Пароли не совпадают";
                MessageBoxViewModel messageBox = new MessageBoxViewModel();
                messageBox.ShowMessageBox(message);
            }
            
               
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
