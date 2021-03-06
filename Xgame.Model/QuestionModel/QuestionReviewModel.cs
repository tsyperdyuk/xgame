﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xgame.Model.QuestionModel
{
    public class QuestionReviewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string QuestionImageUrl { get; set; }
        public string AnswerImageUrl { get; set; }
        public string ApproveStatus { get; set; }
        public string RejectReason { get; set; }
    }
}
