using SmartHome.Data;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class dbViewer : ContentPage
    {
        UserDatabase _database;
        public dbViewer()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserSQLite.db3");
            _database = new UserDatabase(dbPath);
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var users = await _database.GetUsersAsync();
            UsersListView.ItemsSource = users;
        }

        private async void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var user = e.SelectedItem as User;
            if (user != null)
            {
                await DisplayAlert("User Selected", $"Name: {user.FirstName} {user.LastName} \nPass: {user.Password} \nEmail: {user.Email}", "OK");
            }
        }
    }
}