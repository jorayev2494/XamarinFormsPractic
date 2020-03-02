using JsonServer.MVVM.Models;
using JsonServer.Services.Convert;
using JsonServer.Services.RestServer;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UserEditViewModel : BaseMVVM
    {

        private MediaFile mediaFile;

        private User EditUser { get; set; }


        public ICommand UserUpdateCommand { get; private set; }

        #region Select Image Command
        public ICommand SelectImageCommand { get; private set; }
        #endregion

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
            get => EditUser.Avatar;
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
            this.AvatarSource = Task.Run<ImageSource>(async () => await ProjectConverter.UriImgToImageSource(Avatar)).Result;
            UserUpdateCommand = new Command(UserUpdate);
            SelectImageCommand = new Command(SelectImage);
        }

        private async void SelectImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Pick Photo", ":( No Pick Photo available.", "Ok");
                return;
            }

            // Select Image
            mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null) return;

            // Show selected Image
            AvatarSource = ImageSource.FromStream(() => mediaFile.GetStream());
        }

        private async void UserUpdate()
        {

            // FormData
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            // Add data in FormData
            // Many Data file[]
            // formDataContent.Add(new StreamContent(mediaFile.GetStream()), "avatar[]", "photo.jpg");

            if (mediaFile != null)
                formDataContent.Add(new StreamContent(mediaFile.GetStream()), "avatar", "avatar.jpg");

            // Properties
            formDataContent.Add(new StringContent(EditUser.Name), "name");
            formDataContent.Add(new StringContent(EditUser.LastName), "last_name");
            formDataContent.Add(new StringContent(EditUser.Phone), "phone");
            formDataContent.Add(new StringContent(EditUser.Email), "email");

            // Put RestApi
            User updatedUser = await RestApi.POST_FORM_DATA<User>($"/users/{EditUser.Id}", formDataContent);

            if (updatedUser != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User Succes Updated!", "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error Server", "User dont updated!", "Ok");
            }
        }

    }
}
