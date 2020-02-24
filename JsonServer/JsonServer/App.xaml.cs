using JsonServer.MVVM.Views.Users;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);  // This is XF Mateial Design

            // MainPage = new MainPage();
            base.MainPage = new NavigationPage(new UsersListView());
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
