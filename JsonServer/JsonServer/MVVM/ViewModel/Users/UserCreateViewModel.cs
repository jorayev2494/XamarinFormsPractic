using JsonServer.MVVM.Models;
using JsonServer.Services.Convert;
using JsonServer.Services.RestServer;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.ViewModel.Users
{
    public class UserCreateViewModel : BaseMVVM
    {
        private MediaFile mediaFile;

        private User UserCreate = new User();

        #region Upload Image
        // Select Command
        public ICommand SelectImageCommand { get; private set; }

        // Take Photo Command
        public ICommand TakePhotoCommand { get; private set; }

        // Send Image Server Command
        public ICommand UserCreateCommand { get; private set; }
        #endregion

        // Local Base64
        // public string Name
        // {
        //     get => UserCreate.Name;
        //     set
        //     {
        //         if (value != UserCreate.Name)
        //         {
        //             UserCreate.Name = value;
        //             base.OnPropertyChanged("Name");

        //             Avatar = ImageSource.FromStream(() => new MemoryStream(
        //                     Convert.FromBase64String(value)
        //                 )
        //             );
        //         }
        //     }
        // }

        // Online Convert
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
            get => UserCreate.Avatar;
            set
            {
                if (value != UserCreate.Avatar)
                {
                    UserCreate.Avatar = value;
                    base.OnPropertyChanged("Avatar");
                }
            }
        }

        public ImageSource AvatarSource
        {
            get => UserCreate.AvatarSource;
            set
            {
                if (UserCreate.AvatarSource != value)
                {
                    UserCreate.AvatarSource = value;
                    base.OnPropertyChanged("AvatarSource");
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
            // local Base64
            // Name = "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";

            // Default Avatar
            AvatarSource = ImageSource.FromResource("JsonServer.Assets.Images.users.avatar.png");
            //mediaFile.AlbumPath = "JsonServer.Assets.Images.users.avatar.png";

            #region Instance Upload Image Commands
            this.SelectImageCommand = new Command(SelectImage);
            this.TakePhotoCommand = new Command(TakePhoto);
            this.UserCreateCommand = new Command(ImageSendServer);
            #endregion
        }



        /// <summary>
        /// Select Image
        /// </summary>
        private async void SelectImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Pick Photo", ":( No Pick Photo available.", "Ok");
                return;
            }

            this.mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (mediaFile == null) return;

            // Model AvatarSource
            // this.AvatarSource = ImageSource.FromStream(() =>
            // {
            //     return mediaFile.GetStream();
            // });

            this.AvatarSource = mediaFile.Path;

        }

        private async void TakePhoto()
        {

            var cameraPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);


            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
                Directory = "JsonServer",
                Name = "app-json-server-" + DateTime.Now.ToString("dd-mm-ss"),
            });

            if (mediaFile == null)
                return;

            AvatarSource = ImageSource.FromStream(() =>
            {
                return mediaFile.GetStream();
            });
        }

        /// <summary>
        /// POST: Send FormData in Server!
        /// </summary>
        private async void ImageSendServer()
        {
            // FormData
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            // Add data in FormData
            // Many Data file[]
            // formDataContent.Add(new StreamContent(mediaFile.GetStream()), "avatar[]", "photo.jpg");

            // One Data Save Image
            if (mediaFile != null)
                formDataContent.Add(new StreamContent(mediaFile.GetStream()), "avatar", "avatar.jpg");

            // StringContent stringContent = new StringContent(JsonConvert.SerializeObject(UserCreate),
            //                                                 Encoding.UTF8,
            //                                                 "application/json");

            // Properties
            formDataContent.Add(new StringContent(UserCreate.Name), "name");
            formDataContent.Add(new StringContent(UserCreate.LastName), "last_name");
            formDataContent.Add(new StringContent(UserCreate.Phone), "phone");
            formDataContent.Add(new StringContent(UserCreate.Email), "email");

            // Post RestApi
            User userResponse = await RestApi.POST_FORM_DATA<User>("/users", formDataContent);

            if (userResponse != null)
            {
                // UsersViewModel.Users.Add(userResponse);
                await Application.Current.MainPage.DisplayAlert("Success", "User Success Created!", "Ok");
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User dont Created!", "Ok");
            }
        }
    }
}
