using JsonServer.MVVM.Models;
using JsonServer.MVVM.Views.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace JsonServer.MVVM.ViewModel.Db
{
    public class UserDbShowViewModel : BaseMVVM
    {
        public User ShowUser { get; private set; }
        public ICommand UserDbEditCommand { get; private set; }

        public UserDbShowViewModel(User model)
        {
            this.ShowUser = model;
            UserDbEditCommand = new Command(UserDbEdit);
        }

        private async void UserDbEdit()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new UserDbEditView(this.ShowUser));
        }
    }
}
