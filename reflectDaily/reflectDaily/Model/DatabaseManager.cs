using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace reflectDaily.Model
{
    public class DatabaseManager
    {
        private SQLiteConnection db;

        public DatabaseManager(string databasePath)
        {
            db = new SQLiteConnection(databasePath);
            db.CreateTable<PlayerResponse>();
            db.CreateTable<JournalQuestion>();
        }

        public List<PlayerResponse> GetResponsesByDate(DateTime date, int userId)
        {
            var startDate = date.Date; // Midnight at the start of the day
            var endDate = startDate.AddDays(1); // Midnight at the start of the next day

            return db.Table<PlayerResponse>()
                     .Where(r => r.ResponseDate >= startDate && r.ResponseDate < endDate && r.UserId == userId)
                     .ToList();
        }

        public List<PlayerResponse> AddQuestionDetailToPlayerResponses(List<PlayerResponse> responses)


        {
            foreach (PlayerResponse response in responses)
            {

                response.QuestionDetail = GetQuestionDetail(response.QuestionId);

            }
            Console.WriteLine(responses[3].QuestionDetail);
            return responses;

        }
        public string GetQuestionDetail(string questionId)
        {
            var question = db.Table<JournalQuestion>()
                             .Where(r => r.questionNumber == questionId)
                             .First();

            return question?.questionDetail; 
        }
        public void SaveQuestions(List<JournalQuestion> journalQuestions)
        {

            using (SQLiteConnection con = new SQLiteConnection(App.DatabaseLocation))
            {

                foreach (var question in journalQuestions) {

                    try
                    {
                        int row = con.Insert(question);

                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
                con.Close();


            }
        }

        public async Task SaveResponsesToDatabase(List<PlayerResponse> responseList)
        {
            using (var conn = new SQLiteConnection(App.DatabaseLocation))
            {
                foreach (var response in responseList)
                {
                    var existingResponse = conn.Table<PlayerResponse>().FirstOrDefault(r => r.QuestionId == response.QuestionId && r.UserId == response.UserId && r.ResponseDate == response.ResponseDate);
                    if (existingResponse != null)
                    {
                        existingResponse.SelectedOption = response.SelectedOption;
                        conn.Update(existingResponse);
                    }
                    else
                    {
                        conn.Insert(response);

                    }
                }
            }
        }
    }
}
