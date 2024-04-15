using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace reflectDaily.Model
{
    public class PlayerResponse
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string QuestionId { get; set; }

        public string SelectedOption { get; set; }

        public DateTime ResponseDate { get; set; }

        [Ignore]
        public string QuestionDetail { get; set; }
        public override string ToString()
        {
            return ": " + "ID : " + Id + "\n" + " USER ID : "  +  UserId + "\n" + "QUESTION ID : "  + QuestionId + "\n" + " OPTION : " + SelectedOption + "\n"  + "DATE : " + ResponseDate + "Detail" + QuestionDetail;
        }

    }
}
