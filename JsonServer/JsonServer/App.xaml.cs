using JsonServer.MVVM.Views.Users;
using Plugin.Permissions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer
{
    public partial class App : Application
    {

        // public static string URL = "http://192.168.0.113:8080";          // Work Ip
        // public static string URL = "http://192.168.1.109:8080";          // Hostel IP
        public static string URL = "http://192.168.0.104:8080";         // Hostel IP


        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);  // This is XF Mateial Design | 48 33 55 51

            // MainPage = new MainPage();
            base.MainPage = new NavigationPage(new UsersListView());
            // base.MainPage = new NavigationPage(new UserCreateView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
