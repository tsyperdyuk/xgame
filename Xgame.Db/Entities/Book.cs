using System;
using System.Collections.Generic;
using System.Text;

namespace Xgame.Db.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
    }
}
