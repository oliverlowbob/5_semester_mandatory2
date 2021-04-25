using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Models
{
    public class Class
    {
        [Key]
        public int IdClass { get; set; }
        public int Number_Of_Class { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public virtual Course Course { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Lesson> Lessons { get; set; }
    }
}
