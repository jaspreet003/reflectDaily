using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace reflectDaily.Model
{
    public class DatabaseManager
    {
        private SQLiteConnection db;

        public DatabaseManager(string databasePath)
        {
            db = new SQLiteConnection(databasePath);
            db.CreateTable<PlayerResponse>();
        }

        public List<PlayerResponse> GetResponsesByDate(DateTime date, int userId)
        {
            return db.Table<PlayerResponse>()
                     .Where(r => r.ResponseDate.Date == date.Date && r.UserId == userId)
                     .ToList();
        }
    }
}
