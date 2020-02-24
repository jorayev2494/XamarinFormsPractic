using JsonServer.MVVM.Models;
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
    public partial class UserShowView : ContentPage
    {

        public UserShowViewModel ViewModel 
        {
            get => (base.BindingContext as UserShowViewModel);
            set => base.BindingContext = value;
        }

        public UserShowView(User model)
        {
            InitializeComponent();
            this.ViewModel = new UserShowViewModel(model);
        }
    }
}