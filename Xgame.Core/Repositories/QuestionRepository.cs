using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xgame.Core.Repositories;
using Xgame.Db;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Core
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    { 
        public QuestionRepository(XgameContext context) : base(context)
        {
           
        }           

        public IEnumerable<Question> GetAllQuestionsByUserId(string userId)
        {
            return _context.Questions.Where(q => q.AppUserId == userId).OrderByDescending(q => q.Id); //sorting by DESC
        }
    }
}
