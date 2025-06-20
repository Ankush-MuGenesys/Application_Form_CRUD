using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Application_Form
{
    public partial class EditWindow : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppFormDB;Integrated Security=True";
        public EditWindow()
        {
            InitializeComponent();
        }

        

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateUserWindow updateWindow = new UpdateUserWindow();
            updateWindow.ShowDialog();
        }


        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {

            int ID;
            if (!int.TryParse(IdTextBox.Text, out ID))
            {
                MessageBox.Show("Please enter a valid numeric ID.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM UserInfo WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "User deleted successfully!" : "User not found.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error: {ex.Message}");
                }






            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); // Go back to the main window
            mainWindow.Show();
            this.Close(); // Close the current window
        }



    }




}
