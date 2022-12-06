using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techIE.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "sse3f072-d231-e1e1-ab26-1120hhj364e4", 0, "92d74833-7df8-4aa7-ae15-8a58a9cb61e2", "admin@techie.com", false, false, null, "admin@techie.com", "Admin", "AQAAAAEAACcQAAAAEBJmU7Z059iiM1jbWn0asbP/uN+98PKp7Q/IjVK/5tR5jEc655lJzLIRsUJCwRM+Nw==", null, false, "074cf96f-e21b-4f6f-93d9-3291f611d4b3", false, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "sse3f072-d231-e1e1-ab26-1120hhj364e4");
        }
    }
}
