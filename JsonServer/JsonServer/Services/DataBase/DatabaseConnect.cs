using JsonServer.MVVM.Models;
using JsonServer.MVVM.Models.Interfaces;
using JsonServer.Services.DataBase.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;

namespace JsonServer.Services.DataBase
{
    public class DatabaseConnect
    {
        private static SQLiteConnection dbCon;

        public static SQLiteConnection DBConnect<T>() where T : new()
        {
            dbCon = DependencyService.Get<IDatabaseConnection>().SQLiteConnection();
            dbCon.CreateTable<T>();
            return dbCon;
        }

        public static T Find<T>(int id) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Table<T>().FirstOrDefault(d => d.Id == id);
        }

        public static bool Exists<T>(T model) where T : IModel, new()
        {
            return Find<T>(model.Id) != null;

        }

        // #region CRUD
        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Read<T>() where T : new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Table<T>().ToList();
        }

        /// <summary>
        /// CREATE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Create<T>(T model) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Insert(model);
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update<T>(T model) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Update(model);
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Delete<T>(T model) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Delete(model);
        }
        // #endregion



    }
}
