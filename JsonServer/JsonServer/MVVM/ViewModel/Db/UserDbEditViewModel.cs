using JsonServer.MVVM.Models;
using JsonServer.Services.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Db
{
    public class UserDbEditViewModel : BaseMVVM
    {
        private User EditUser { get; set; }
        public ICommand UserDbUpdateCommand { get; private set; }

        // Image
        private int imageWidth = 255;

        public int ImageWidth 
        { 
            get => imageWidth;
            private set 
            {
                if (imageWidth != value)
                {
                    imageWidth = value;
                    base.OnPropertyChanged("ImageWidth");
                }
            }
        }

        public ICommand ImageTapGestureCommand { get; private set; }

        public string MyProperty { get; set; }

        public string Name
        {
            get => EditUser.Name;
            set
            {
                if (EditUser.Name != value)
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
                if (EditUser.LastName != value)
                {
                    EditUser.LastName = value;
                    base.OnPropertyChanged("LastName");
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

        
        // Poviye && Bestruke
        public string Avatar
        {
            get => EditUser.Avatar;
            set
            {
                if (EditUser.Avatar != value)
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
                    base.OnPropertyChanged("Avatar");
                }
            }
        }

        public UserDbEditViewModel(User model)
        {
            this.EditUser = model;

            // Update Command
            this.UserDbUpdateCommand = new Command(async () => {
                User updatedUser = DatabaseConnect.Update<User>(this.EditUser);

                if (updatedUser != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Db Succes", $"User : {updatedUser.Name} success updated!", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync(false);
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Db Error", $"User : {EditUser.Name} dont updated!", "Ok");
                }
            });

            // Image Command
            this.ImageTapGestureCommand = new Command(() => {
                if (ImageWidth == 255) ImageWidth = 200;
                else ImageWidth = 255;
                Application.Current.MainPage.DisplayAlert("Image", $"Double click: {EditUser.Avatar}!", "Ok");
            });
        }



    }
}
