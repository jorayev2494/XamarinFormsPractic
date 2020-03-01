using JsonServer.MVVM.Models;
using JsonServer.Services.RestServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UserEditViewModel : BaseMVVM
    {

        private User EditUser { get; set; }

        public ICommand UserUpdateCommand { get; private set; }

        public string Name
        {
            get => EditUser.Name;
            set
            {
                if (value != EditUser.Name)
                {
                    EditUser.Name = value;
                    base.OnPropertyChanged("Name");
                }
            }
        }

        public string LastName
        {
            get => EditUser.LastName;
            set
            {
                if (value != EditUser.LastName)
                {
                    EditUser.LastName = value;
                    base.OnPropertyChanged("LastName");
                }
            }
        }

        public string Avatar
        {
            get => App.URL + EditUser.Avatar;
            set
            {
                if (value != EditUser.Avatar)
                {
                    EditUser.Avatar = value;
                    base.OnPropertyChanged("Avatar");
                }
            }
        }

        public ImageSource AvatarSource
        {
            get => EditUser.AvatarSource;
            set
            {
                if (EditUser.AvatarSource != value)
                {
                    EditUser.AvatarSource = value;
                    base.OnPropertyChanged("AvatarSource");
                }
            }
        }

        public string Email
        {
            get => EditUser.Email;
            set
            {
                if (EditUser.Email != value)
                {
                    EditUser.Email = value;
                    base.OnPropertyChanged("Email");
                }
            }
        }

        public string Phone
        {
            get => EditUser.Phone;
            set
            {
                if (EditUser.Phone != value)
                {
                    EditUser.Phone = value;
                    base.OnPropertyChanged("Phone");
                }
            }
        }
        

        public UserEditViewModel(User model)
        {
            this.EditUser = model;
            UserUpdateCommand = new Command(UserUpdate);
        }

        private async void UserUpdate()
        {

            User updatedUser = await RestApi.PUT<User>("/users", EditUser);

            if (updatedUser != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User Succes Updated!", "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Error Server", "User dont updated!", "Ok");
        }

    }
}
