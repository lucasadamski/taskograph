using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace taskograph.EF.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Durations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Minutes = table.Column<int>(type: "int", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    Days = table.Column<int>(type: "int", nullable: true),
                    Weeks = table.Column<int>(type: "int", nullable: true),
                    Months = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Groups_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    DateId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    DurationId = table.Column<int>(type: "int", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entries_Durations_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Durations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entries_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreciseTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreciseTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreciseTargets_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreciseTargets_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegularTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TargetDurationId = table.Column<int>(type: "int", nullable: false),
                    PerTimeframeDurationId = table.Column<int>(type: "int", nullable: false),
                    DateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularTargets_Dates_DateId",
                        column: x => x.DateId,
                        principalTable: "Dates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegularTargets_Durations_PerTimeframeDurationId",
                        column: x => x.PerTimeframeDurationId,
                        principalTable: "Durations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegularTargets_Durations_TargetDurationId",
                        column: x => x.TargetDurationId,
                        principalTable: "Durations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegularTargets_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Red" },
                    { 2, "Green" },
                    { 3, "Blue" },
                    { 4, "Yellow" },
                    { 5, "Grey" },
                    { 6, "Brown" },
                    { 7, "Orange" },
                    { 8, "Pink" },
                    { 9, "Purple" },
                    { 10, "Beige" }
                });

            migrationBuilder.InsertData(
                table: "Dates",
                columns: new[] { "Id", "Created", "Deleted", "LastUpdated" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6864), null, null },
                    { 2, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6912), null, null },
                    { 3, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6914), null, null },
                    { 4, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6915), null, null },
                    { 5, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6917), null, null },
                    { 6, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6918), null, null },
                    { 7, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6920), null, null },
                    { 8, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6921), null, null },
                    { 9, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6923), null, null },
                    { 10, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6925), null, null },
                    { 11, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6926), null, null },
                    { 12, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6928), null, null },
                    { 13, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6929), null, null },
                    { 14, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6931), null, null },
                    { 15, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6932), null, null },
                    { 16, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6934), null, null },
                    { 17, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6935), null, null },
                    { 18, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6937), null, null },
                    { 19, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6938), null, null },
                    { 20, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6940), null, null },
                    { 21, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6941), null, null },
                    { 22, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6943), null, null },
                    { 23, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6944), null, null },
                    { 24, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6946), null, null },
                    { 25, new DateTime(2024, 3, 29, 18, 44, 20, 996, DateTimeKind.Local).AddTicks(6947), null, null }
                });

            migrationBuilder.InsertData(
                table: "Durations",
                columns: new[] { "Id", "Days", "Hours", "Minutes", "Months", "Weeks" },
                values: new object[,]
                {
                    { 1, null, null, 5, null, null },
                    { 2, null, null, 10, null, null },
                    { 3, null, null, 15, null, null },
                    { 4, null, null, 30, null, null },
                    { 5, null, null, 45, null, null },
                    { 6, null, 1, null, null, null },
                    { 7, null, 2, null, null, null },
                    { 8, null, 3, null, null, null },
                    { 9, null, 4, null, null, null },
                    { 10, null, 5, null, null, null },
                    { 11, null, 6, null, null, null },
                    { 12, null, 12, null, null, null },
                    { 13, 1, null, null, null, null },
                    { 14, 2, null, null, null, null },
                    { 15, 3, null, null, null, null },
                    { 16, 4, null, null, null, null },
                    { 17, 5, null, null, null, null },
                    { 18, 6, null, null, null, null },
                    { 19, null, null, null, null, 1 },
                    { 20, null, null, null, null, 2 },
                    { 21, null, null, null, null, 3 },
                    { 22, null, null, null, 1, null },
                    { 23, null, null, null, 2, null },
                    { 24, null, null, null, 3, null },
                    { 25, null, null, null, 6, null },
                    { 26, null, null, null, 12, null },
                    { 27, null, null, null, 24, null },
                    { 28, null, null, null, 48, null }
                });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "What you have to do today is insignificant, but is very important that you do it." },
                    { 2, "It's about the marathon, not the sprint." },
                    { 3, "Don't feel bad because you don't know something and feel like you can't do anything. Do what you can do and then improve." }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "AlarmClock", "Off" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ColorId", "DateId", "Name" },
                values: new object[,]
                {
                    { 1, null, 9, "Health" },
                    { 2, null, 10, "Education" },
                    { 3, null, 11, "FriendsAndFamily" },
                    { 4, null, 12, "Sport" },
                    { 5, null, 13, "Work" },
                    { 6, null, 14, "Hobby" },
                    { 7, null, 15, "Relaxation" },
                    { 8, null, 16, "Entertaiment" },
                    { 9, null, 17, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "PreciseTargets",
                columns: new[] { "Id", "DateDue", "DateId", "Name", "TaskId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Read Little Prince", null },
                    { 2, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Run 10 km", null }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "ColorId", "DateId", "GroupId", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, null, 5, 4, "Running", "1" },
                    { 2, null, 6, 2, "Reading", "1" },
                    { 3, null, 7, 1, "Cooking", "1" },
                    { 4, null, 8, 7, "Dancing", "1" }
                });

            migrationBuilder.InsertData(
                table: "RegularTargets",
                columns: new[] { "Id", "DateId", "PerTimeframeDurationId", "TargetDurationId", "TaskId" },
                values: new object[,]
                {
                    { 1, 3, 13, 3, 1 },
                    { 2, 4, 14, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_DateId",
                table: "Entries",
                column: "DateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_DurationId",
                table: "Entries",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_TaskId",
                table: "Entries",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ColorId",
                table: "Groups",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_DateId",
                table: "Groups",
                column: "DateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreciseTargets_DateId",
                table: "PreciseTargets",
                column: "DateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreciseTargets_TaskId",
                table: "PreciseTargets",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularTargets_DateId",
                table: "RegularTargets",
                column: "DateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegularTargets_PerTimeframeDurationId",
                table: "RegularTargets",
                column: "PerTimeframeDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularTargets_TargetDurationId",
                table: "RegularTargets",
                column: "TargetDurationId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularTargets_TaskId",
                table: "RegularTargets",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ColorId",
                table: "Tasks",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_DateId",
                table: "Tasks",
                column: "DateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GroupId",
                table: "Tasks",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "PreciseTargets");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "RegularTargets");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Durations");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Dates");
        }
    }
}
