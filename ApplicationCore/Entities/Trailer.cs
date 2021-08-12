using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    //if someone give you a trailerId  56 => name of trailer, url, title, revenue
    // you can navigarte from trailer object to movie object 
    // it is like a join table 

    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        //many one trailer belong to one movie
        //Foreignkey
        //look at any entity call movie, for that table the id is primary key.  automatically make it as foreign key
        public int MovieId { get; set; }
        //Build into entity framework to do that for us
        public Movie Movie { get; set; }
        //why do we need object, because this is navigation property 
        //

    }
}
