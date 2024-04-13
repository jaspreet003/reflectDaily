using reflectDaily.Main.journal;
using SQLite;
using System;
using System.Collections;
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

        public List<PlayerResponse> GetResponsesByDate(DateTime strDate, DateTime endDate, int userId)
        {
            Console.WriteLine($"Initial Dates: {strDate.ToString()} , {endDate.ToString()} ");
            var startDate = strDate.Date; // Midnight at the start of the day
            var nextDayEndDate = endDate.Date; // Midnight at the start of the next day

            // If the start and end dates are the same, adjust the end date to be inclusive
            if (startDate == nextDayEndDate)
            {
                nextDayEndDate = nextDayEndDate.AddDays(1);
                Console.WriteLine($"Same Dates: {startDate.ToString()} , {nextDayEndDate.ToString()} ");
                var list1 = db.Table<PlayerResponse>()
                           .Where(r => r.ResponseDate >= startDate && r.ResponseDate < nextDayEndDate && r.UserId == userId).ToList();
                foreach (var item in list1)
                {
                    Console.WriteLine(item.ToString());
                }

                return list1;
            }
            else
            {
                Console.WriteLine($"Different Dates: {startDate.ToString()} , {nextDayEndDate.ToString()} ");

                var list2 =  db.Table<PlayerResponse>()
                           .Where(r => r.ResponseDate >= startDate && r.ResponseDate <= nextDayEndDate && r.UserId == userId).ToList();

                foreach (var item in list2)
                {
                    Console.WriteLine(item.ToString());
                }

                return list2;
            }

           
        }



        public List<PlayerResponse> AddQuestionDetailToPlayerResponses(List<PlayerResponse> responses)


        {
            foreach (PlayerResponse response in responses)
            {

                response.QuestionDetail = GetQuestionDetail(response.QuestionId);

            }
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
