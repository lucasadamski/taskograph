﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using taskograph.EF.DataAccess;

#nullable disable

namespace taskograph.EF.Migrations
{
    [DbContext(typeof(TasksContext))]
    [Migration("20240527141641_WithoutColorInTask")]
    partial class WithoutColorInTask
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("taskograph.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("taskograph.Models.Tables.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = "123123123123"
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Colors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Red"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Green"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Blue"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Yellow"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Grey"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Brown"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Orange"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Pink"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Purple"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Beige"
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Duration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("Minutes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Durations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Minutes = 5L
                        },
                        new
                        {
                            Id = 2,
                            Minutes = 10L
                        },
                        new
                        {
                            Id = 3,
                            Minutes = 15L
                        },
                        new
                        {
                            Id = 4,
                            Minutes = 30L
                        },
                        new
                        {
                            Id = 5,
                            Minutes = 45L
                        },
                        new
                        {
                            Id = 6,
                            Minutes = 60L
                        },
                        new
                        {
                            Id = 7,
                            Minutes = 75L
                        },
                        new
                        {
                            Id = 8,
                            Minutes = 90L
                        },
                        new
                        {
                            Id = 9,
                            Minutes = 105L
                        },
                        new
                        {
                            Id = 10,
                            Minutes = 120L
                        },
                        new
                        {
                            Id = 11,
                            Minutes = 135L
                        },
                        new
                        {
                            Id = 12,
                            Minutes = 150L
                        },
                        new
                        {
                            Id = 13,
                            Minutes = 165L
                        },
                        new
                        {
                            Id = 14,
                            Minutes = 180L
                        },
                        new
                        {
                            Id = 15,
                            Minutes = 195L
                        },
                        new
                        {
                            Id = 16,
                            Minutes = 210L
                        },
                        new
                        {
                            Id = 17,
                            Minutes = 225L
                        },
                        new
                        {
                            Id = 18,
                            Minutes = 240L
                        },
                        new
                        {
                            Id = 19,
                            Minutes = 300L
                        },
                        new
                        {
                            Id = 20,
                            Minutes = 360L
                        },
                        new
                        {
                            Id = 21,
                            Minutes = 420L
                        },
                        new
                        {
                            Id = 22,
                            Minutes = 480L
                        },
                        new
                        {
                            Id = 23,
                            Minutes = 720L
                        },
                        new
                        {
                            Id = 24,
                            Minutes = 1440L
                        },
                        new
                        {
                            Id = 25,
                            Minutes = 2880L
                        },
                        new
                        {
                            Id = 26,
                            Minutes = 4320L
                        },
                        new
                        {
                            Id = 27,
                            Minutes = 5760L
                        },
                        new
                        {
                            Id = 28,
                            Minutes = 7200L
                        },
                        new
                        {
                            Id = 29,
                            Minutes = 8640L
                        },
                        new
                        {
                            Id = 30,
                            Minutes = 10080L
                        },
                        new
                        {
                            Id = 31,
                            Minutes = 20160L
                        },
                        new
                        {
                            Id = 32,
                            Minutes = 30240L
                        },
                        new
                        {
                            Id = 33,
                            Minutes = 40320L
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<int>("DurationId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.HasIndex("TaskId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("ColorId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Health"
                        },
                        new
                        {
                            Id = 2,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Education"
                        },
                        new
                        {
                            Id = 3,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "FriendsAndFamily"
                        },
                        new
                        {
                            Id = 4,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 5,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Work"
                        },
                        new
                        {
                            Id = 6,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Hobby"
                        },
                        new
                        {
                            Id = 7,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Relaxation"
                        },
                        new
                        {
                            Id = 8,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Entertaiment"
                        },
                        new
                        {
                            Id = 9,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            Name = "Finance"
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.PreciseTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("PreciseTargets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            DateDue = new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Read Little Prince",
                            TaskId = 1
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            DateDue = new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Run 10 km",
                            TaskId = 2
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Quotes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppUserId = 1,
                            Name = "What you have to do today is insignificant, but is very important that you do it."
                        },
                        new
                        {
                            Id = 2,
                            AppUserId = 1,
                            Name = "It's about the marathon, not the sprint."
                        },
                        new
                        {
                            Id = 3,
                            AppUserId = 1,
                            Name = "Don't feel bad because you don't know something and feel like you can't do anything. Do what you can do and then improve."
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.RegularTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("PerTimeframeDurationId")
                        .HasColumnType("int");

                    b.Property<int>("TargetDurationId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PerTimeframeDurationId");

                    b.HasIndex("TargetDurationId");

                    b.HasIndex("TaskId");

                    b.ToTable("RegularTargets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            PerTimeframeDurationId = 13,
                            TargetDurationId = 3,
                            TaskId = 1
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            PerTimeframeDurationId = 14,
                            TargetDurationId = 4,
                            TaskId = 2
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppUserId = 1,
                            Name = "AlarmClock",
                            Value = "Off"
                        });
                });

            modelBuilder.Entity("taskograph.Models.Tables.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("GroupId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            GroupId = 4,
                            Name = "Running"
                        },
                        new
                        {
                            Id = 2,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            GroupId = 2,
                            Name = "Reading"
                        },
                        new
                        {
                            Id = 3,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            GroupId = 1,
                            Name = "Cooking"
                        },
                        new
                        {
                            Id = 4,
                            AppUserId = 1,
                            Created = new DateTime(2024, 5, 27, 0, 0, 0, 0, DateTimeKind.Local),
                            GroupId = 7,
                            Name = "Dancing"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("taskograph.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("taskograph.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("taskograph.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("taskograph.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("taskograph.Models.Tables.Entry", b =>
                {
                    b.HasOne("taskograph.Models.Tables.Duration", "Duration")
                        .WithMany("Entries")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("taskograph.Models.Tables.Task", "Task")
                        .WithMany("Entries")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Duration");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Group", b =>
                {
                    b.HasOne("taskograph.Models.Tables.AppUser", "AppUser")
                        .WithMany("Groups")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("taskograph.Models.Tables.Color", "Color")
                        .WithMany("Groups")
                        .HasForeignKey("ColorId");

                    b.Navigation("AppUser");

                    b.Navigation("Color");
                });

            modelBuilder.Entity("taskograph.Models.Tables.PreciseTarget", b =>
                {
                    b.HasOne("taskograph.Models.Tables.Task", "Task")
                        .WithMany("PreciseTargets")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Quote", b =>
                {
                    b.HasOne("taskograph.Models.Tables.AppUser", "AppUser")
                        .WithMany("Quotes")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("taskograph.Models.Tables.RegularTarget", b =>
                {
                    b.HasOne("taskograph.Models.Tables.Duration", "PerTimeframeDuration")
                        .WithMany("PerTimeframeRegularTargets")
                        .HasForeignKey("PerTimeframeDurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("taskograph.Models.Tables.Duration", "TargetDuration")
                        .WithMany("TargetRegularTargets")
                        .HasForeignKey("TargetDurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("taskograph.Models.Tables.Task", "Task")
                        .WithMany("RegularTargets")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PerTimeframeDuration");

                    b.Navigation("TargetDuration");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Setting", b =>
                {
                    b.HasOne("taskograph.Models.Tables.AppUser", "AppUser")
                        .WithMany("Settings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Task", b =>
                {
                    b.HasOne("taskograph.Models.Tables.AppUser", "AppUser")
                        .WithMany("Tasks")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("taskograph.Models.Tables.Group", "Group")
                        .WithMany("Tasks")
                        .HasForeignKey("GroupId");

                    b.Navigation("AppUser");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("taskograph.Models.Tables.AppUser", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Quotes");

                    b.Navigation("Settings");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Color", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Duration", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("PerTimeframeRegularTargets");

                    b.Navigation("TargetRegularTargets");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Group", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("taskograph.Models.Tables.Task", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("PreciseTargets");

                    b.Navigation("RegularTargets");
                });
#pragma warning restore 612, 618
        }
    }
}
