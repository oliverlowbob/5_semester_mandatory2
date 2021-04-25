using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        public virtual User User { get; set; }

        public virtual Class Class { get; set; }
        public virtual Boolean Present { get; set; }

    }
}
