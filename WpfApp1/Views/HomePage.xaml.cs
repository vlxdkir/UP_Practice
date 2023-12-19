using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private MainViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //MainFrame.Content = new EditPage();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=ONLYUP; Initial Catalog=MVVMLoginDb; Integrated Security=True");
            connection.Open();
            string cmd = "SELECT Id, Username, Password, Email, AccessLevel FROM UsersTrain";
            SqlDataAdapter dataAdp = new SqlDataAdapter(cmd, connection);
            DataTable dt = new DataTable("UsersTrain");
            dataAdp.Fill(dt);

            // Очистите содержимое DataGridView
            DGUsers.ItemsSource = null;

            DGUsers.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        public void DeleteUsername(int Id)
        {
            SqlConnection connection = new SqlConnection("Data Source=ONLYUP;Initial Catalog=MVVMLoginDb;Integrated Security=True");
            string cmd = "DELETE FROM [UsersTrain] WHERE Id = @Id";
            SqlCommand deleteCommand = new SqlCommand(cmd, connection);
            deleteCommand.Parameters.AddWithValue("@Id", Id);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DGUsers.SelectedItem != null && DGUsers.SelectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)DGUsers.SelectedItem;
                int Id = Convert.ToInt32(row["Id"]);
                DeleteUsername(Id);
            }
        }
    }
}
