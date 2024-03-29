using System;
using System.Collections.Generic;
using System.Text;

namespace reflectDaily.Model
{
    public class JournalQuestion
    {
        public int questionNumber { get; set; }
        public String questionDetail { get; set; }
        public List<String> options { get; set; }

        public JournalQuestion() {

            this.questionNumber = 1;
            this.questionDetail = "How much you exercise today?";
            this.options = new List<String>() { 
                "10 Min",
                "20 Min",
                "30 Min",
                "40 Min",
                "50 Min",
                "1 Hour",
                "1- 2 Hour",
                "More Than 2 Hours",
                "Did not practise today"
            };
        }
    }
}
