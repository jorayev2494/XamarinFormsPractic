using JsonServer.MVVM.Models;
using JsonServer.MVVM.Views.DB;
using JsonServer.MVVM.Views.Users;
using JsonServer.Services.DataBase;
using JsonServer.Services.RestServer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UsersViewModel : BaseMVVM
    {

        public ObservableCollection<User> Users { get; private set; }

        public ICommand ItemSelectedCommand { get; private set; }
        public ICommand GoUserCreateCommand { get; private set; }
        public ICommand UserDeleteCommand { get; private set; }
        public ICommand RefreshUsersListCommand { get; private set; }

        // Database
        public ICommand SaveLocalDbCpmmand { get; private set; }
        public ICommand GoDbUsersCommand { get; private set; }


        private bool isRefreshing = false;

        public bool IsRefreshing 
        {
            get => isRefreshing;
            private set 
            {
                isRefreshing = value;
                base.OnPropertyChanged(nameof(this.IsRefreshing));
            } 
        }

        public UsersViewModel()
        {
            this.Users = new ObservableCollection<User>();

            this.ItemSelectedCommand = new Command<User>(u => ItemSeleccted(u));
            this.GoUserCreateCommand = new Command(async () => {
                await Application.Current.MainPage.Navigation.PushAsync(new UserCreateView());
            });

            this.UserDeleteCommand = new Command<User>(async (u) => {
                bool isDeleted = await RestApi.DELETE<User>("/users", u);

                if (isDeleted)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", $"User: {u.Name} success deleted!", "Ok");
                    // await Application.Current.MainPage.Navigation.PopToRootAsync();
                    // this.LoadUsers();
                    Users.Remove(u);
                }
                else await Application.Current.MainPage.DisplayAlert("Error", $"User: {u.Name} dont deleted!", "Ok");
            });

            this.RefreshUsersListCommand = new Command(LoadUsers);

            // Database
            this.SaveLocalDbCpmmand = new Command<User>(u => SaveLocalDb(u));
            this.GoDbUsersCommand = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new UsersDbListView()));

            this.LoadUsers();
        }

        public async void LoadUsers()
        {
            // this.IsRefreshing = !this.IsRefreshing;
            this.IsRefreshing = true;

            // IEnumerable<User> RestUsers = Task.Run(async () => await RestApi.GET<User>("/users")).Result;

            IEnumerable<User> RestUsers = await RestApi.GET<User>("/users");

            Users.Clear();

            // Adding RestUsers in ObserverCollection
            foreach (User user in RestUsers)
            {
                user.Avatar = "http://192.168.1.108:8080" + user.Avatar;
                this.Users.Add(user);
            }
 
            // this.IsRefreshing = !this.IsRefreshing;
            this.IsRefreshing = false;
        }

        private async void ItemSeleccted(User model)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserShowView(model));
        }

        // DataBase
        private async void SaveLocalDb(User model)
        {
            if (DatabaseConnect.Exists<User>(model))
            {
                await Application.Current.MainPage.DisplayAlert("Info", model.Name + " uze saved!", "Ok");
            }
            else
            {
                int userId = DatabaseConnect.Create<User>(model);
                if (userId > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Db Success", model.Name + " success saved in db!", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Db Error", model.Name + " dont saved in db!", "Ok");
                }
            }
        }
    }
}
