using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonServer.MVVM.Models.Interfaces
{
    public interface IModel
    {
        [PrimaryKey, AutoIncrement]
        int Id { get; set; }
    }   
}
