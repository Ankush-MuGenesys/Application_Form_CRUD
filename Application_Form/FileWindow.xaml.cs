using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Application_Form
{
    public partial class FileWindow : Window
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AppFormDB;Integrated Security=True";

        public FileWindow()
        {
            InitializeComponent();
        }

        public class UserInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }

        private List<UserInfo> GetAllUsers()
        {
            List<UserInfo> users = new List<UserInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ID, Name, Email, Address FROM UserInfo";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new UserInfo
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
            }
            return users;
        }

        private void SaveDataAsJson(string filePath)
        {
            List<UserInfo> users = GetAllUsers();
            string jsonData = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonData);
            MessageBox.Show("User data saved as JSON!");
        }

        private void SaveDataAsBinary(string filePath)
        {
            List<UserInfo> users = GetAllUsers();
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (var user in users)
                {
                    writer.Write(user.ID);
                    writer.Write(user.Name);
                    writer.Write(user.Email);
                    writer.Write(user.Address);
                }
            }
            MessageBox.Show("User data saved in Binary format!");
        }

        private void GetUserData_Click(object sender, RoutedEventArgs e)
        {
            //string jsonPath = "UserData.json";
            //string binaryPath = "UserData.bin";

            string jsonPath = "C:\\Users\\admin\\Downloads\\AppUserData\\UserData.json";

            string binaryPath = @"C:\Users\admin\Downloads\AppUserData\UserData.json";



            SaveDataAsJson(jsonPath);
            SaveDataAsBinary(binaryPath);
        }




        //=====================================CSV FILE ========================================================

        private void SaveDataAsCsv(string filePath)
        {
            List<UserInfo> users = GetAllUsers();
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("ID,Name,Email,Address");

            foreach (var user in users)
            {
                csvContent.AppendLine($"{user.ID},{user.Name},{user.Email},{user.Address}");
            }

            File.WriteAllText(filePath, csvContent.ToString());
            MessageBox.Show("User data saved as CSV!");
        }


        private void ViewCsv_Click(object sender, RoutedEventArgs e)
        {
            string csvPath = @"C:\Users\admin\Downloads\UserData.csv";
            if (File.Exists(csvPath))
            {
                System.Diagnostics.Process.Start("notepad.exe", csvPath); // Opens CSV in Notepad
            }
            else
            {
                MessageBox.Show("CSV file not found!");
            }
        }

        private void DownloadCsv_Click(object sender, RoutedEventArgs e)
        {
            string csvPath = @"C:\Users\admin\Downloads\UserData.csv";
            SaveDataAsCsv(csvPath);
        }

    }
}