using Android.Graphics;
using JsonServer.MVVM.ViewModel.Users;
using JsonServer.Services.RestServer;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCreateView : ContentPage
    {
        // private MediaFile mediaFile;
        public string DefaultAvatar { get; set; }

        public UserCreateViewModel ViewModel 
        {
            get => (base.BindingContext as UserCreateViewModel); 
            set => base.BindingContext = value;
        }

        public UserCreateView()
        {
            InitializeComponent();
            this.ViewModel = new UserCreateViewModel() { Avatar = $"{App.URL}/storage/images/photo.jpg" };
        }

        // Pik Photo Clicked
        //private async void Button_select(object sender, EventArgs e)
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        await DisplayAlert("No Pick Photo", ":( No Pick Photo available.", "Ok");
        //        return;
        //    }

        //    mediaFile = await CrossMedia.Current.PickPhotoAsync();

        //    //if (mediaFile == null) return;
        //    if (mediaFile == null) 
        //        await DisplayAlert("Error Photo", ":( No Pick Photo Sukaaaa.", "aaaa");

        //    // Path Image
        //    base.Title = mediaFile.Path;

        //    // Model Avatar
        //    //testImg.Source = ImageSource.FromStream(() =>
        //    //{
        //    //    return mediaFile.GetStream();
        //    //});
        //}

        private void Button_camera(object sender, EventArgs e)
        {

        }

        //// Send Iamge in Server Laravel
        //private async void Button_sendSever(object sender, EventArgs e)
        //{
        //    // URL - send file
        //    string serverFileUrl = $"{App.URL}/api/file";

        //    // FormData
        //    MultipartFormDataContent formDataContent = new MultipartFormDataContent();
        //    // Add Form Data
        //    formDataContent.Add(new StreamContent(mediaFile.GetStream()), "file", $"{mediaFile.Path}");

        //    // Instance HttpClient
        //    HttpClient fileHttpClient = new HttpClient();
        //    // Send Server
        //    HttpResponseMessage httpResponseMessage = await fileHttpClient.PostAsync(serverFileUrl, formDataContent);
        //    // Read as String Server Response 
        //    string content = await httpResponseMessage.Content.ReadAsStringAsync();

        //    // Convert to MobileResult
        //    MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

        //    // testImg.Source = ImageSource.FromUri(new Uri($"{App.URL}/storage/images/default.jpg"));
        //    base.Title = mobileResult.Data.ToString();

        //    //base.Title = JsonConvert.DeserializeObject<string>(mobileResult.Data.ToString());

        //}
    }
}