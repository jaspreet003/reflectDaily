﻿using System;
using System.Collections.Generic;
using System.Text;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace reflectDaily.Model
{
    public class JournalQuestion
    {
        public string questionNumber { get; set; }
        public string questionDetail { get; set; }
        public List<string> options { get; set; }

    }

    
}
