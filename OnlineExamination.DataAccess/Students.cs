using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineExamination.DataAccess
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        
        public int? GroupsId { get; set; }
        public Groups Groups { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; } = new HashSet<ExamResults>();

    }
}
