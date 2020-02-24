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
    public partial class UserEditView : ContentPage
    {
        public UserEditViewModel ViewModel 
        { 
            get => (base.BindingContext as UserEditViewModel);
            set => base.BindingContext = value;
        }

        public UserEditView(User model)
        {
            InitializeComponent();
            this.ViewModel = new UserEditViewModel(model);
        }
    }
}