using System;
using System.Collections.Generic;
using System.Text;

namespace reflectDaily.Model
{
    public class JournalQuestion
    {
        public String questionNumber { get; set; }
        public String questionDetail { get; set; }
        public List<String> options { get; set; }

    }
}
