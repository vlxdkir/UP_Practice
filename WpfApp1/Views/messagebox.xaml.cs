﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для messagebox.xaml
    /// </summary>
    public partial class messagebox : Window
    {
        public static messagebox messageBox;
        public messagebox()
        {

            InitializeComponent();
            messageBox = this;
        }

        public void HandleMessageBox(object sender, string message)
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
            messageBoxViewModel.Message = message;
            messageBox.DataContext = messageBoxViewModel;
            messageBox.ShowDialog();

        }
    }
}
