using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using taskograph.Models;
using taskograph.Models.Tables;
using Task = taskograph.Models.Tables.Task;


namespace taskograph.EF.DataAccess
{
   public class TasksContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Models.Tables.Group> Groups { get; set; }
        public DbSet<PreciseTarget> PreciseTargets { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<RegularTarget> RegularTargets { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //data base with users  

            modelBuilder.Entity<ApplicationUser>()
                .Property(n => n.FirstName)
                .HasMaxLength(25);

            modelBuilder.Entity<ApplicationUser>()
                .Property(n => n.LastName)
                .HasMaxLength(25);

            modelBuilder.Entity<RegularTarget>().HasOne(n => n.Task)
               .WithMany(n => n.RegularTargets)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PreciseTarget>().HasOne(n => n.Task)
               .WithMany(n => n.PreciseTargets)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>().HasMany(n => n.Tasks)
                .WithOne(n => n.ApplicationUser)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ApplicationUser>().HasMany(n => n.Groups)
                .WithOne(n => n.ApplicationUser)
               .OnDelete(DeleteBehavior.NoAction);

            DateTime dayToday = DateTime.Today;

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser()
                {
                    Id = "none",
                    UserName = "none",
                    NormalizedUserName = "NONE",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                    FirstName = "none",
                    LastName = "none"
                }
                );

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "Red" },
                new Color { Id = 2, Name = "Green" },
                new Color { Id = 3, Name = "Blue" },
                new Color { Id = 4, Name = "Yellow" },
                new Color { Id = 5, Name = "Grey" },
                new Color { Id = 6, Name = "Brown" },
                new Color { Id = 7, Name = "Orange" },
                new Color { Id = 8, Name = "Pink" },
                new Color { Id = 9, Name = "Purple" },
                new Color { Id = 10, Name = "Beige" }
                );
  

            modelBuilder.Entity<Quote>().HasData(
                 new Quote { Id = 1, Name = "What you have to do today is insignificant, but is very important that you do it.", ApplicationUserId = "none" },
                 new Quote { Id = 2, Name = "It's about the marathon, not the sprint.", ApplicationUserId = "none" },
                 new Quote { Id = 3, Name = "Don't feel bad because you don't know something and feel like you can't do anything. Do what you can do and then improve.", ApplicationUserId = "none" }
                 );

            //TODO stworz uzytkownia w AspNetUsers z id "none"

            modelBuilder.Entity<Models.Tables.Group>().HasData(
                new Models.Tables.Group { Id = 1, Name = "Health", Created = dayToday, ApplicationUserId = "none" },
                new Models.Tables.Group { Id = 2, Name = "Education", Created = dayToday , ApplicationUserId = "none"},
                new Models.Tables.Group { Id = 3, Name = "FriendsAndFamily", Created = dayToday, ApplicationUserId = "none" },
                new Models.Tables.Group { Id = 4, Name = "Sport", Created = dayToday, ApplicationUserId = "none" },
                new Models.Tables.Group { Id = 5, Name = "Work", Created = dayToday, ApplicationUserId = "none" },
                new Models.Tables.Group { Id = 6, Name = "Hobby", Created = dayToday, ApplicationUserId = "none" },
                new Models.Tables.Group { Id = 7, Name = "Relaxation", Created = dayToday , ApplicationUserId = "none"},
                new Models.Tables.Group { Id = 8, Name = "Entertaiment", Created = dayToday , ApplicationUserId = "none"},
                new Models.Tables.Group { Id = 9, Name = "Finance", Created = dayToday, ApplicationUserId = "none" }
                );

            modelBuilder.Entity<Task>().HasData(
              new Task { Id = 1, Name = "Running", GroupId = 4,   Created = dayToday, ApplicationUserId = "none" },
              new Task { Id = 2, Name = "Reading", GroupId = 2,   Created = dayToday , ApplicationUserId = "none"},
              new Task { Id = 3, Name = "Cooking", GroupId = 1,   Created = dayToday , ApplicationUserId = "none"},
              new Task { Id = 4, Name = "Dancing", GroupId = 7,   Created = dayToday , ApplicationUserId = "none"}
              );

            modelBuilder.Entity<PreciseTarget>().HasData(
                    new PreciseTarget { Id = 1, TaskId = 1, Name = "Read Little Prince", DateDue = new DateTime(2024, 06, 01), Created = dayToday},
                    new PreciseTarget { Id = 2, TaskId = 2,Name = "Run 10 km", DateDue = new DateTime(2024, 05, 10), Created = dayToday }
                    );

            modelBuilder.Entity<RegularTarget>().HasData(
              new RegularTarget { Id = 1, TaskId = 1, TimeDedicatedToPerformTarget = 10, RegularTimeIntervalToAchieveTarget =  60, Created = dayToday },
              new RegularTarget { Id = 2, TaskId = 2, TimeDedicatedToPerformTarget = 5, RegularTimeIntervalToAchieveTarget = 60,  Created = dayToday  }
              );

            modelBuilder.Entity<Setting>().HasData(
              new Setting { Id = 1, Name = "AlarmClock" , Value = "Off", ApplicationUserId = "none" }
              );

        }
    }
}
