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
    public partial class UpdateUserWindow : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppFormDB;Integrated Security=True";

        public UpdateUserWindow()
        {
            InitializeComponent();
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (IdTextBox == null || NameTextBox == null || EmailTextBox == null || AddressTextBox == null)
            {
                MessageBox.Show("Error: One or more input fields are missing.");
                return;
            }

            int ID;
            if (!int.TryParse(IdTextBox.Text.Trim(), out ID))
            {
                MessageBox.Show("Please enter a valid numeric ID.");
                return;
            }

            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE UserInfo SET Name = @Name, Email = @Email, Address = @Address WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "User updated successfully!" : "User not found.");

                    this.Close(); // Close form after successful update
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error: {ex.Message}");
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow(); // Go back to the main window
            editWindow.Show();
            this.Close(); // Close the current window
        }
    }


}
