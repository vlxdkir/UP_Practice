using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    internal class MessageBoxViewModel : ViewModelBase
    {
        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand CloseCommand { get; private set; }
        public MessageBoxViewModel()
        {
            CloseCommand = new ViewModelCommand(CloseMessageBox);
        }

        private void CloseMessageBox(object parameter)
        {
            messagebox messageBox = messagebox.messageBox;
            messageBox.Close();
        }

        public void ShowMessageBox(string message)
        {
            MessageBoxViewModel viewModel = new MessageBoxViewModel { Message = message };
            messagebox view = new messagebox { DataContext = viewModel };
            view.ShowDialog();
        }
    }
}
