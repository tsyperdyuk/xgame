using System;
using System.Collections.Generic;

namespace Xgame.Model
{
    public class PagedList<T>
    {
        public int Count { get; set; }

        public IList<T> List { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}