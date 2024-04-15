using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace reflectDaily.Model
{
    public class JournalQuestion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string questionNumber { get; set; }
        public string questionDetail { get; set; }

        [Ignore]
        public List<string> options { get; set; }


        public override string ToString()
        {
            return ": " + "ID : " + questionNumber + "\n" + "QUESTION Detail : " + questionDetail + "\n" + " OPTION : " + options[1];
        }

    }

}
