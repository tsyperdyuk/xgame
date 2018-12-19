using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xgame.Core.Repositories;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Core
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetAllQuestionsByUserId(string userId);
        Task<List<Question>> GetAllQuestions();
        Task<PagedList<Question>> GetQuestions(int pageIndex, int pageSize, string status);
        Task Delete(Task<Question> question);
    }
}
