using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Application_Form
{
    public partial class ViewWindow : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppFormDB;Integrated Security=True";

        public ViewWindow()
        {
            InitializeComponent();
            

        }

        private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
        {
            UserListBox.Items.Clear(); // Clear old data
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID, Name, Email, Address FROM UserInfo";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string userInfo = $"ID: {reader["ID"]}, Name: {reader["Name"]}, Email: {reader["Email"]}, Address: {reader["Address"]}";
                        UserListBox.Items.Add(userInfo);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving data: {ex.Message}");
                }
            }
            LoadUsers();//------------------------------------------------------------
        }




        private void LoadUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID, Name, Email, Address FROM UserInfo";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    var users = new List<User>();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Address = reader.GetString(3)
                        });
                    }

                    UserDataGrid.ItemsSource = users;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error loading users: " + ex.Message);
                }
            }
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}