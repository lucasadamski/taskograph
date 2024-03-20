using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskograph.Models.Tables;


namespace taskograph.EF.DataAccess
{
   public class TasksContext : IdentityDbContext //TODO Indentity
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Duration> Durations { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PreciseTarget> PreciseTargets { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<RegularTarget> RegularTargets { get; set; }
        public DbSet<Models.Tables.Task> Tasks { get; set; }


        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Date>().HasData(
               new Date { Id = 1, Created = DateTime.Now },
               new Date { Id = 2, Created = DateTime.Now },
               new Date { Id = 3, Created = DateTime.Now },
               new Date { Id = 4, Created = DateTime.Now },
               new Date { Id = 5, Created = DateTime.Now },
               new Date { Id = 6, Created = DateTime.Now },
               new Date { Id = 7, Created = DateTime.Now },
               new Date { Id = 8, Created = DateTime.Now },
               new Date { Id = 9, Created = DateTime.Now },
               new Date { Id = 10, Created = DateTime.Now },
               new Date { Id = 11, Created = DateTime.Now },
               new Date { Id = 12, Created = DateTime.Now },
               new Date { Id = 13, Created = DateTime.Now },
               new Date { Id = 14, Created = DateTime.Now },
               new Date { Id = 15, Created = DateTime.Now },
               new Date { Id = 16, Created = DateTime.Now },
               new Date { Id = 17, Created = DateTime.Now },
               new Date { Id = 18, Created = DateTime.Now },
               new Date { Id = 19, Created = DateTime.Now },
               new Date { Id = 20, Created = DateTime.Now },
               new Date { Id = 21, Created = DateTime.Now },
               new Date { Id = 22, Created = DateTime.Now },
               new Date { Id = 23, Created = DateTime.Now },
               new Date { Id = 24, Created = DateTime.Now },
               new Date { Id = 25, Created = DateTime.Now }
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
               new Duration { Id = 1, End = new DateTime(1, 1, 1, 0, 1, 0) },
               new Duration { Id = 2, End = new DateTime(1, 1, 1, 0, 5, 0) },
               new Duration { Id = 3, End = new DateTime(1, 1, 1, 0, 10, 0) },
               new Duration { Id = 4, End = new DateTime(1, 1, 1, 0, 15, 0) },
               new Duration { Id = 5, End = new DateTime(1, 1, 1, 0, 30, 0) },
               new Duration { Id = 6, End = new DateTime(1, 1, 1, 0, 45, 0) },
               new Duration { Id = 7, End = new DateTime(1, 1, 1, 1, 0, 0) },
               new Duration { Id = 8, End = new DateTime(1, 1, 1, 2, 0, 0) },
               new Duration { Id = 9, End = new DateTime(1, 1, 1, 3, 0, 0) },
               new Duration { Id = 10, End = new DateTime(1, 1, 1, 4, 0, 0) },
               new Duration { Id = 11, End = new DateTime(1, 1, 1, 5, 0, 0) },
               new Duration { Id = 12, End = new DateTime(1, 1, 1, 10, 0, 0) },
               new Duration { Id = 13, End = new DateTime(1, 1, 2, 0, 0, 0) },
               new Duration { Id = 14, End = new DateTime(1, 1, 3, 0, 0, 0) },
               new Duration { Id = 15, End = new DateTime(1, 1, 4, 0, 0, 0) },
               new Duration { Id = 16, End = new DateTime(1, 1, 5, 0, 0, 0) },
               new Duration { Id = 17, End = new DateTime(1, 1, 6, 0, 0, 0) },
               new Duration { Id = 18, End = new DateTime(1, 1, 7, 0, 0, 0) },
               new Duration { Id = 19, End = new DateTime(1, 1, 14, 0, 0, 0)},
               new Duration { Id = 20, End = new DateTime(1, 2, 1, 0, 0, 0) },
               new Duration { Id = 21, End = new DateTime(1, 3, 1, 0, 0, 0) },
               new Duration { Id = 22, End = new DateTime(1, 4, 1, 0, 0, 0) },
               new Duration { Id = 23, End = new DateTime(1, 5, 1, 0, 0, 0) },
               new Duration { Id = 24, End = new DateTime(1, 6, 1, 0, 0, 0) },
               new Duration { Id = 25, End = new DateTime(2, 0, 1, 0, 0, 0) }
               );

            

            modelBuilder.Entity<PreciseTarget>().HasData(
                    new PreciseTarget { Id = 1, Name = "Read Little Prince", DateDue = new DateTime(2024, 06, 01) , DateId = 1},
                    new PreciseTarget { Id = 2, Name = "Run 10 km", DateDue = new DateTime(2024, 05, 10), DateId = 2 }
                    );

            modelBuilder.Entity<Quote>().HasData(
                 new Quote { Id = 1, Name = "What you have to do today is insignificant, but is very important that you do it." },
                 new Quote { Id = 2, Name = "It's about the marathon, not the sprint." },
                 new Quote { Id = 3, Name = "Don't feel bad because you don't know something and feel like you can't do anything. Do what you can do and then improve." }
                 );
            modelBuilder.Entity<Models.Tables.Task>().HasData(
              new Models.Tables.Task { Id = 1, Name = "Running", GroupId = 4, DateId = 5 },
              new Models.Tables.Task { Id = 2, Name = "Reading", GroupId = 2, DateId = 6 },
              new Models.Tables.Task { Id = 3, Name = "Cooking", GroupId = 1, DateId = 7 },
              new Models.Tables.Task { Id = 4, Name = "Dancing", GroupId = 7, DateId = 8 }
              );

            modelBuilder.Entity<RegularTarget>().HasData(
              new RegularTarget { Id = 1, TaskId = 1, TargetDurationId = 3, PerTimeframeDurationId =  13, DateId = 3},
              new RegularTarget { Id = 2, TaskId = 2, TargetDurationId = 4, PerTimeframeDurationId = 14, DateId = 4 }
              );

            modelBuilder.Entity<Setting>().HasData(
              new Setting { Id = 1, Name = "AlarmClock" , Value = "Off"}
              );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Health", DateId = 9 },
                new Group { Id = 2, Name = "Education", DateId = 10 },
                new Group { Id = 3, Name = "FriendsAndFamily", DateId = 11 },
                new Group { Id = 4, Name = "Sport", DateId = 12 },
                new Group { Id = 5, Name = "Work", DateId = 13 },
                new Group { Id = 6, Name = "Hobby", DateId = 14 },
                new Group { Id = 7, Name = "Relaxation", DateId = 15 },
                new Group { Id = 8, Name = "Entertaiment", DateId = 16 },
                new Group { Id = 9, Name = "Finance", DateId = 17 }
                );

        }
    }
}
