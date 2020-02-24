using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JsonServer.MVVM.ViewModel
{
    public class BaseMVVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propretyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName: propretyName));
        }
    }
}
