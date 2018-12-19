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

        public async Task<List<Question>> GetAllQuestionsByUserId(string userId)
        {
            return await _context.Questions.Where(q => q.AppUserId == userId).OrderByDescending(q => q.Id).ToListAsync(); //sorting by DESC
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            return await _context.Questions.Include(u => u.User).OrderByDescending(q => q.Id).ToListAsync(); //sorting by DESC
        }

        public async Task<PagedList<Question>> GetQuestions(int pageIndex, int pageSize, string status)
        {
            var skip = (pageIndex - 1) * pageSize;

            var filterQuery = _context.Questions
                .Include(u => u.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                filterQuery = filterQuery.Where(q => q.Status == status);
            }

            var pageQuery = filterQuery.Skip(skip).Take(pageSize);

            var count = await filterQuery.CountAsync();
            var list = await pageQuery.ToListAsync();

            return new PagedList<Question>
            {
                List = list,
                Count = count,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public Task Delete(Task<Question> question)
        {
            throw new NotImplementedException();
        }
    }
}
