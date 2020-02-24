using JsonServer.MVVM.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.Views.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCreateView : ContentPage
    {
        public UserCreateViewModel ViewModel 
        {
            get => (base.BindingContext as UserCreateViewModel); 
            set => base.BindingContext = value;
        }

        public UserCreateView()
        {
            InitializeComponent();
            this.ViewModel = new UserCreateViewModel();
        }
    }
}