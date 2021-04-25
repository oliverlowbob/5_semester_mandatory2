using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.DbContexts
{
    public class MyDBContext : DbContext  
    {  
        public DbSet<User> User { get; set; }  
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Class> Class { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)  
        {   
        }  


  
    }  

}
