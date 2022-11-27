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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsAdmin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "sse3f072-d231-e1e1-ab26-1120hhj364e4", 0, "1c8042a4-f2c2-4d5e-8032-76e241f42ce4", "admin@techie.com", false, false, false, null, "admin@techie.com", "Admin", "AQAAAAEAACcQAAAAEL7e8Dd5XHcCeVc8RzcC5E/KLqkvmGwSKmdvnSlJvlL9I5zfl+nI9Ui4p2Ce70t7ZA==", null, false, "67867c3a-cfc1-42f1-907e-5f9c19da69be", false, "Admin" });
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
