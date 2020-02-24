using JsonServer.MVVM.Models;
using JsonServer.Services.RestServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UserCreateViewModel : BaseMVVM
    {

        private User UserCreate = new User();

        public ICommand UserCreateCommand { get; private set; }

        public string Name
        {
            get => UserCreate.Name;
            set
            {
                if (value != UserCreate.Name)
                {
                    UserCreate.Name = value;
                    base.OnPropertyChanged("Name");
                }
            }
        }

        public string LastName
        {
            get => UserCreate.LastName;
            set
            {
                if (value != UserCreate.LastName)
                {
                    UserCreate.LastName = value;
                    base.OnPropertyChanged("LastName");
                }
            }
        }

        public string Avatar
        {
            get => "http://192.168.1.108:8080/storage/images/default.jpg";
            set
            {
                if (value != UserCreate.Avatar)
                {
                    UserCreate.Avatar = value;
                    base.OnPropertyChanged("Avatar");
                }
            }
        }

        public string Email
        {
            get => UserCreate.Email;
            set
            {
                if (UserCreate.Email != value)
                {
                    UserCreate.Email = value;
                    base.OnPropertyChanged("Email");
                }
            }
        }

        public string Phone
        {
            get => UserCreate.Phone;
            set
            {
                if (UserCreate.Phone != value)
                {
                    UserCreate.Phone = value;
                    base.OnPropertyChanged("Phone");
                }
            }
        }

        public UserCreateViewModel()
        {
            // this.UserCreateCommand = new Command(CreateUser, () => {
            //     return Name.Length > 1
            //         && LastName.Length > 1
            //         && Email.Length > 1
            //         && Phone.Length > 1;
            // });

            this.UserCreateCommand = new Command(CreateUser);
        }

        private async void CreateUser()
        {
            User userCreated = await RestApi.POST<User>("/users", this.UserCreate);

            if (userCreated != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User Success Created!", "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error", "User dont Created!", "Ok");

        }
    }
}
