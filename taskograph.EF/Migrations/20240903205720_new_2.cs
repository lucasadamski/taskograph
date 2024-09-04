using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskograph.EF.Migrations
{
    /// <inheritdoc />
    public partial class new_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Tasks_TaskId",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "none",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89d24605-fc04-4b2a-bece-a23f5a5f18eb", "AQAAAAIAAYagAAAAEEN/R+KwoPgMOAvw6OT03yt0hGm0mZ4O760W6Phtj6w/zZ13YyYfs5Caeb9P3bv5Dg==", "a0ba9919-d23b-4f6c-8005-40b80c950626" });

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Tasks_TaskId",
                table: "Entries",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Tasks_TaskId",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "Entries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "none",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "868e3418-949d-4ff2-b40f-b481e85cb4da", "AQAAAAIAAYagAAAAELTdy+rjgNuHey82NHxhpemAtw1Q0Z9qPUWavCddlZooD3dJr3VbDmm+4EjTEYSXIw==", "e80530a3-8b05-4aa6-a055-f0e8c3889dd7" });

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Tasks_TaskId",
                table: "Entries",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
