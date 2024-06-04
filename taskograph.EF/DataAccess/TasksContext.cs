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
        public DbSet<Duration> Durations { get; set; }
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

            modelBuilder.Entity<Entry>().HasOne(n => n.Duration)
                .WithMany(n => n.Entries)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Entry>().HasOne(n => n.Task)
                .WithMany(n => n.Entries)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegularTarget>().HasOne(n => n.TimeDedicatedToPerformTarget)
                .WithMany(n => n.TargetRegularTargets)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegularTarget>().HasOne(n => n.RegularTimeIntervalToAchieveTarget)
                .WithMany(n => n.PerTimeframeRegularTargets)
                .OnDelete(DeleteBehavior.NoAction);

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

            modelBuilder.Entity<Duration>().HasData(
               new Duration { Id = 1, Minutes = 5 },
               new Duration { Id = 2, Minutes = 10 },
               new Duration { Id = 3, Minutes = 15  },
               new Duration { Id = 4, Minutes = 30  },
               new Duration { Id = 5, Minutes = 45  },  
               new Duration { Id = 6, Minutes = 60 },     //1:00
               new Duration { Id = 7, Minutes = 75 },     //1:15
               new Duration { Id = 8, Minutes = 90 },     //1:30
               new Duration { Id = 9, Minutes = 105 },    //1:45
               new Duration { Id = 10, Minutes = 120 },   //2:00
               new Duration { Id = 11, Minutes = 135 },   //2:15
               new Duration { Id = 12, Minutes = 150 },   //2:30
               new Duration { Id = 13, Minutes = 165 },   //2:45
               new Duration { Id = 14, Minutes = 180 },   //3:00
               new Duration { Id = 15, Minutes = 195 },   //3:15
               new Duration { Id = 16, Minutes = 210 },   //3:30
               new Duration { Id = 17, Minutes = 225 },   //3:45
               new Duration { Id = 18, Minutes = 240 },   //4:00
               new Duration { Id = 19, Minutes = 300 },   //5:00
               new Duration { Id = 20, Minutes = 360 },   //6:00
               new Duration { Id = 21, Minutes = 420 },   //7:00
               new Duration { Id = 22, Minutes = 480 },   //8:00
               new Duration { Id = 23, Minutes = 720 },   //12:00
               new Duration { Id = 24, Minutes = 1440 },   //1 day
               new Duration { Id = 25, Minutes = 2880 },   //2 days
               new Duration { Id = 26, Minutes = 4320 },   //3 days
               new Duration { Id = 27, Minutes = 5760 },   //4 days
               new Duration { Id = 28, Minutes = 7200 },   //5 days
               new Duration { Id = 29, Minutes = 8640 },   //6 days
               new Duration { Id = 30, Minutes = 10080 },   //1 week
               new Duration { Id = 31, Minutes = 20160 },   //2 weeks
               new Duration { Id = 32, Minutes = 30240 },   //3 weeks
               new Duration { Id = 33, Minutes = 40320 }     //1 month
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
              new RegularTarget { Id = 1, TaskId = 1, TimeDedicatedToPerformTargetId = 3, RegularTimeIntervalToAchieveTargetId =  13, Created = dayToday },
              new RegularTarget { Id = 2, TaskId = 2, TimeDedicatedToPerformTargetId = 4, RegularTimeIntervalToAchieveTargetId = 14,  Created = dayToday  }
              );

            modelBuilder.Entity<Setting>().HasData(
              new Setting { Id = 1, Name = "AlarmClock" , Value = "Off", ApplicationUserId = "none" }
              );

            ///modyfy quote, update database, chceck if AspNetUsers are wired correctly


        }
    }
}
