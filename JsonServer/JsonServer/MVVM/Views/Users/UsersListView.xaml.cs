using JsonServer.MVVM.Models;
using JsonServer.MVVM.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersListView : ContentPage
    {

        public UsersViewModel ViewModel 
        {
            get => (base.BindingContext as UsersViewModel);
            set => base.BindingContext = value;
        }

        public UsersListView()
        {
            InitializeComponent();
            this.ViewModel = new UsersViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            User itemSelected = e.SelectedItem as User;

            if (itemSelected != null)
                this.ViewModel.ItemSelectedCommand.Execute(itemSelected);

            listView.SelectedItem = null;
        }

        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        //     this.ViewModel.LoadUsers();
        // }
    }
}