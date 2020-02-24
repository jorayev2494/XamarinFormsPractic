using JsonServer.MVVM.Models;
using JsonServer.MVVM.ViewModel.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.Views.DB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersDbListView : ContentPage
    {

        public UsersDbListViewModel ViewModel 
        { 
            get => base.BindingContext as UsersDbListViewModel; 
            set => base.BindingContext = value; 
        }

        public UsersDbListView()
        {
            InitializeComponent();
            ViewModel = new UsersDbListViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            User selectedUser = e.SelectedItem as User;

            if (selectedUser != null)
               ViewModel.UserDbDeleteCommand.Execute(selectedUser);

            listView.SelectedItem = null;
        }
    }
}