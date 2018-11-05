using System;
using System.Collections.Generic;
using System.Text;
using Xgame.Core.Repositories;
using Xgame.Db.Entities;

namespace Xgame.Core
{
    public interface IQuestionRepository : IRepository<Question>
    {
        IEnumerable<Question> GetAllQuestionsByUserId(string userId);
        IEnumerable<Question> GetAllQuestions();
    }
}
