using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonServer.Services.DataBase.Interfaces
{
    public interface IDatabaseConnection
    {

        SQLiteConnection SQLiteConnection();

    }
}
