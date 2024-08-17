using Microsoft.EntityFrameworkCore;
using NewPetProjectC_.TasksEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPetProjectC_
{
    public class EntityContext : DbContext
    {
        public EntityContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("YOUR CONNECTION STRING");
        }

        public DbSet<TaskEntity> BasicTasks { get; set; }
        public DbSet<TimeEntity> TimeTasks { get; set; }
    }
}
