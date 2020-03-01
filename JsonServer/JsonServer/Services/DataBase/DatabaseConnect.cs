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

        // Find
        public static T Find<T>(int id) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            return connection.Table<T>().FirstOrDefault(d => d.Id == id);
        }

        // Exists
        public static bool Exists<T>(T model) where T : IModel, new()
        {
            return Find<T>(model.Id) != null;

        }

        // Count
        public static int Count<T>() where T : new()
        {
            return Read<T>().Count();
        }

        // #region CRUD
        /// <summary>
        /// Read
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
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
        public static T Update<T>(T model) where T : IModel, new()
        {
            SQLiteConnection connection = DBConnect<T>();
            connection.Update(model);
            return Find<T>(model.Id);
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

        /// <summary>
        /// CLEAR DataBase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void CLEAR<T>() where T : new()
        {
            DBConnect<T>().DeleteAll<T>();
            // return connection.DeleteAll<T>();
        }
        // #endregion



    }
}
