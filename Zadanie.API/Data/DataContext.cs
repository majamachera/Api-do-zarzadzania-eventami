using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zadanie.API.Models;

namespace Zadanie.API.Data

{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<Events> Events { get; set; }
        public DbSet<User> User { get; set; }

        
    }
}