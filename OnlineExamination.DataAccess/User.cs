﻿using System;
using System.Collections.Generic;

namespace OnlineExamination.DataAccess
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public ICollection<Groups> Groups { get; set; } = new HashSet<Groups>();
    }
}
