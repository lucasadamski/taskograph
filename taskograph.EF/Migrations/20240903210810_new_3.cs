using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taskograph.EF.Migrations
{
    /// <inheritdoc />
    public partial class new_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "none",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42df009e-a4b8-4a9d-94e2-7493f01b2533", "AQAAAAIAAYagAAAAEJqKs+zIDI1GkNv1DnFlGNpw8xZK6KsGfVLZl69BoX7STeYPP42sipzyUnPZ2nGXEg==", "561c2109-7300-4677-9341-985bb605f8ea" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "none",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89d24605-fc04-4b2a-bece-a23f5a5f18eb", "AQAAAAIAAYagAAAAEEN/R+KwoPgMOAvw6OT03yt0hGm0mZ4O760W6Phtj6w/zZ13YyYfs5Caeb9P3bv5Dg==", "a0ba9919-d23b-4f6c-8005-40b80c950626" });
        }
    }
}
