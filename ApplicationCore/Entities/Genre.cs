using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    //data annotation
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
        //[required] not null
        public ICollection<Movie> Movies { get; set; }
        //we need this for many to many relationships
        //didn't make a table fore genre, data annotation 


    }
}

//connect to our datacontext to use entities to represent these entities as a table