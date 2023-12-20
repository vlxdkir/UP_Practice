using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Models;
using WpfApp1.Repos;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;
        private ObservableCollection<UserModel> users;
        public UserRepository dbLog = new UserRepository();

        private int _id;
        private string _email;
        private string _password;
        private string _username;
        private int _acceslevel;
        private string _name;
        private string _lastname;

        private int currentAccessLevel;
        private Visibility _stackPanelVisibility = Visibility.Hidden;

        private IUserRepository userEditor;
        private UserModel _selectedUser;

        public Visibility StackPanelVisibility
        {
            get { return _stackPanelVisibility; }
            set
            {
                _stackPanelVisibility = value;
                OnPropertyChanged(nameof(StackPanelVisibility));
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


        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public int AccessLevel
        {
            get
            {
                return _acceslevel;
            }
            set
            {
                _acceslevel = value;
                OnPropertyChanged(nameof(AccessLevel));
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

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
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

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        public UserModel Selecteduser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(Selecteduser));
            }
        }






        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
            UpdateUserCollection();
            EditCommand = new ViewModelCommand(Edit);
            userEditor = new UserRepository();
            PageEditUser = new ViewModelCommand(OpenPageEdit);
            DeleteUser = new ViewModelCommand(DeleteSelectedUser);
        }

        public ViewModelCommand EditCommand { get; private set; }
        public ViewModelCommand PageEditUser { get; private set; }
        public ViewModelCommand DeleteUser { get; private set; }

        private void Edit(object parameter)
        {
            userEditor.EditUser(Username, Password, Name, Lastname, Email, CurrentAccessLevel);
            string message = "Пользователь отредактирован";
            MessageBoxViewModel messageBox = new MessageBoxViewModel();
            messageBox.ShowMessageBox(message);
            UpdateUserCollection();
            
        }

        private void DeleteSelectedUser(object parameter)
        {
            userRepository.DeleteUsername(Selecteduser.Id);
            string message = "Пользователь Удален";
            MessageBoxViewModel messageBox = new MessageBoxViewModel();
            messageBox.ShowMessageBox(message);
            UpdateUserCollection();

        }


        private void OpenPageEdit(object parameter)
        {
            Username = Selecteduser.Username;
            Password = Selecteduser.Password;
            Name = Selecteduser.Name;
            Lastname = Selecteduser.LastName; 
            Email = Selecteduser.Email;
            AccessLevel = Selecteduser.AccessLevel;
        }
        private void UpdateUserCollection()
        {
            Users = new ObservableCollection<UserModel>(userRepository.GetAllUsers());
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
