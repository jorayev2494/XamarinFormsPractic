using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JsonServer.Droid.Dependencies;
using JsonServer.Services.DataBase.Interfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace JsonServer.Droid.Dependencies
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection SQLiteConnection()
        {
            string dbName = "database.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentPath, dbName);
            return new SQLiteConnection(path);
        }
    }
}