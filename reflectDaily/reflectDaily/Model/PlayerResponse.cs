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


        public override string ToString()
        {
            return ": " + "ID : " + Id + "\n" + " USER ID : "  +  UserId + "\n" + "QUESTION ID : "  + QuestionId + "\n" + " OPTION : " + SelectedOption + "\n"  + "DATE : " + ResponseDate;
        }

        public static string GetQuestionDetailFromJson(string questionId, string jsonFilePath)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(JournalQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(jsonFilePath);
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var questions = JsonConvert.DeserializeObject<List<JournalQuestion>>(json);

                // Find the question that matches the questionId
                var question = questions.FirstOrDefault(q => q.questionNumber == questionId);
                return question?.questionDetail; // This will be null if no match is found
            }
        }

    }
}
