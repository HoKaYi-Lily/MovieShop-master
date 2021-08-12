using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Review
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal? Rating { get; set; }
        public string ReviewText { get; set; }
        //it has two foreign key as it is id, combo
        //pass in object
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
