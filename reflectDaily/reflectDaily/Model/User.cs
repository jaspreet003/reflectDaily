using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace reflectDaily.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public static int UpdateUser(User user, string databaseLocation)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(databaseLocation)) throw new ArgumentException("Invalid database location.", nameof(databaseLocation));

            using (var db = new SQLiteConnection(databaseLocation))
            {
                db.CreateTable<User>();
                return db.Update(user);
            }
        }
    }
}
