using System;
//using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace Application_Form
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = AppFormDB; Integrated Security = True";
        public MainWindow()
        {
            InitializeComponent();

        }

        

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int ID;
            if (!int.TryParse(IdTextBox.Text, out ID))
            {
                MessageBox.Show("Invalid ID format. Please enter a numeric ID.");
                return;
            }

            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string address = AddressTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("All fields are required. Please enter valid data.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO UserInfo (ID, Name, Email, Address) VALUES (@ID, @Name, @Email, @Address)";
                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ID;
                    cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 100).Value = email;
                    cmd.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar, 255).Value = address;

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show($"Data Saved Successfully! User ID: {ID}");
                    else
                        MessageBox.Show("No data inserted! Check inputs and try again.");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) // Primary key violation (duplicate ID)
                    {
                        MessageBox.Show($"Error: The entered ID '{ID}' already exists. Please enter a unique ID.");
                    }
                    else
                    {
                        MessageBox.Show($"SQL Error ({ex.Number}): {ex.Message}");
                    }
                }
            }
            //LoadUsers(); //-------------------------------------------------------------------------------------

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = string.Empty;
            IdTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
        }

        private void ShowImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility
            DisplayedImage.Visibility = Visibility.Visible;
        }

        private void HideImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility
            DisplayedImage.Visibility = Visibility.Collapsed;
        }

        private void FileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FileWindow fileWindow = new FileWindow();
            fileWindow.Show();
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
           
            EditWindow editWindow = new EditWindow();
            editWindow.Show();

        }


        private void ViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
        }




       


    }




  

}