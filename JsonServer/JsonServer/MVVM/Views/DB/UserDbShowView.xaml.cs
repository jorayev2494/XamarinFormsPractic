using JsonServer.MVVM.Models;
using JsonServer.MVVM.ViewModel.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonServer.MVVM.Views.DB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDbShowView : ContentPage
    {

        public UserDbShowViewModel ViewModel 
        { 
            get => (base.BindingContext as UserDbShowViewModel);
            set => base.BindingContext = value;
        }

        public UserDbShowView(User model)
        {
            InitializeComponent();
            this.ViewModel = new UserDbShowViewModel(model);
        }
    }
}