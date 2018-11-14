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
        public string QuestionImageUrl { get; set; }
        public string AnswerImageUrl { get; set; }
        public string ApproveStatus { get; set; }
        public string RejectReason { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public string AppUserId { get; set; }     
        public AppUser User { get; set; } 
    }
}
