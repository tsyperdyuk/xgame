using System;
using System.Collections.Generic;
using System.Text;

namespace Xgame.Db.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public byte[] QuestionImage { get; set; }
        public byte[] AnswerImage { get; set; }

        public int AppUserId { get; set; } 
        public AppUser User { get; set; } 
    }
}
