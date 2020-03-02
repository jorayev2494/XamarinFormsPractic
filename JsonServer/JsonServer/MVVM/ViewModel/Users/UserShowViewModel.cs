﻿using JsonServer.MVVM.Models;
using JsonServer.MVVM.Views.Users;
using JsonServer.Services.Convert;
using JsonServer.Services.RestServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UserShowViewModel : BaseMVVM
    {

        public User ShowUser { get; private set; }
        public ICommand UserEditCommand { get; set; }

        public string Name 
        { 
            get => ShowUser.Name;
            set 
            {
                if (value != ShowUser.Name)
                {
                    ShowUser.Name = value;
                    base.OnPropertyChanged("Name");
                }
            }
        }

        public string LastName
        {
            get => ShowUser.LastName;
            set
            {
                if (value != ShowUser.LastName)
                {
                    ShowUser.LastName = value;
                    base.OnPropertyChanged("LastName");
                }
            }
        }

        public string Avatar
        {
            get => ShowUser.Avatar;
            set
            {
                if (value != ShowUser.Avatar)
                {
                    ShowUser.Avatar = value;
                    base.OnPropertyChanged("Avatar");
                }
            }
        }

        public ImageSource AvatarSource 
        { 
            get => ShowUser.AvatarSource;
            set {
                if (ShowUser.AvatarSource != value)
                {
                    ShowUser.AvatarSource = value;
                    base.OnPropertyChanged("AvatarSource");
                }
            } 
        }

        public string Email 
        {
            get => ShowUser.Email;
            set
            {
                if (ShowUser.Email != value)
                {
                    ShowUser.Email = value;
                    base.OnPropertyChanged("Email");
                }
            }
        }

        public string Phone 
        {
            get => ShowUser.Phone;
            set 
            {
                if (ShowUser.Phone != value)
                {
                    ShowUser.Phone = value;
                    base.OnPropertyChanged("Phone");
                }
            }
        }

        public UserShowViewModel(User model)
        {
            this.ShowUser = model;
            this.AvatarSource = Task.Run<ImageSource>(async () => await ProjectConverter.UriImgToImageSource(Avatar)).Result;
            UserEditCommand = new Command(UserEdit, () => ShowUser.Email != "admin@admin.com");
        }

        private async void UserEdit()
        {
            // string selected = await Application.Current.MainPage.DisplayActionSheet("Edit User", "Cancel", string.Format("Description: {0}", ShowUser.Email), "params", "params2", "params3");
            // await Application.Current.MainPage.DisplayAlert("Selected", string.Format("Item: {0}", selected), "Ok");
            
            await Application.Current.MainPage.Navigation.PushAsync(new UserEditView(ShowUser));
        }

    }
}
