using System;

namespace Xgame.Model
{
    public class QuestionRepresentModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string QuestionImageUrl { get; set; }
        public string AnswerImageUrl { get; set; }
        public string AppUserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
    }
}
