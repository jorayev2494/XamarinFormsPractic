using JsonServer.MVVM.Models;
using JsonServer.MVVM.Views.Users;
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
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else await Application.Current.MainPage.DisplayAlert("Error", $"User: {u.Name} dont deleted!", "Ok");
            });

            this.RefreshUsersListCommand = new Command(LoadUsers);

            this.LoadUsers();
        }

        public async void LoadUsers()
        {
            this.IsRefreshing = !this.IsRefreshing;

            // IEnumerable<User> RestUsers = Task.Run(async () => await RestApi.GET<User>("/users")).Result;

            IEnumerable<User> RestUsers = await RestApi.GET<User>("/users");

            Users.Clear();

            // Adding RestUsers in ObserverCollection
            foreach (User user in RestUsers)
            {
                this.Users.Add(user);
            }
 
            this.IsRefreshing = !this.IsRefreshing;
        }

        private async void ItemSeleccted(User model)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserShowView(model));
        }
    }
}
