using System;
using System.Collections.Generic;


namespace Skoleprotokol.Models
{
    public class Class
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual Course Course { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Lesson> Lessons { get; set; }
    }
}
