﻿using SQLite;
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
    }
}
