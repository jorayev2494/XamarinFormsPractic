using JsonServer.MVVM.Models;
using JsonServer.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Db
{
    public class UsersDbListViewModel : BaseMVVM
    {
        public ObservableCollection<User> DbUsers { get; private set; }

        // Refreshing
        public bool isDbRefreshing = false;

        public bool IsDdRefreshing 
        {
            get => isDbRefreshing;
            private set {
                isDbRefreshing = value;
                base.OnPropertyChanged(nameof(IsDdRefreshing));
            }
        }

        public ICommand DbRedreshingCommand { get; private set; }
        public ICommand UserDbDeleteCommand { get; private set; }

        public UsersDbListViewModel()
        {
            DbUsers = new ObservableCollection<User>();
            this.DbRedreshingCommand = new Command(DbLoadUsers);
            this.UserDbDeleteCommand = new Command<User>(u => UserDbDelete(u));
            this.DbLoadUsers();
        }

        public void DbLoadUsers()
        {
            this.IsDdRefreshing = !this.IsDdRefreshing;

            DbUsers.Clear();

            IEnumerable<User> dbUser = DatabaseConnect.Read<User>();

            foreach (User user in dbUser) DbUsers.Add(user);

            this.IsDdRefreshing = !this.IsDdRefreshing;
        }

        private async void UserDbDelete(User model)
        {
            int userId = DatabaseConnect.Delete<User>(model);

            if (userId > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Deleted", string.Format("User Deleted id: {0}", userId), "Ok");
                DbLoadUsers();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Db Error", string.Format("User Dont Deleted id: {0}", model.Name), "Ok");
            }
        }
    }
}
