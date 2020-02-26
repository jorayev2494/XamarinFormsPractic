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
    public partial class UserDbEditView : ContentPage
    {

        public UserDbEditViewModel ViewModel {
            get => base.BindingContext as UserDbEditViewModel;
            set => base.BindingContext = value; 
        }

        public UserDbEditView(User model)
        {
            InitializeComponent();
            ViewModel = new UserDbEditViewModel(model);
        }
    }
}