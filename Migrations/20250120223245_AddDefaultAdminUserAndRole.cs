using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminRoleId = Guid.NewGuid();
            migrationBuilder.InsertData(
                table: "Rols",
                columns: ["Id", "Name"],
                values: [adminRoleId, "Admin"]
            );

            var adminUserId = Guid.NewGuid();
            migrationBuilder.InsertData(
                table: "Users",
                columns: ["Id", "Name", "Alias", "RolId"],
                values: [adminUserId, "Default Admin", "admin", adminRoleId]
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "Users",
               keyColumn: "Alias",
               keyValue: "admin"
           );

            migrationBuilder.DeleteData(
                table: "Rols",
                keyColumn: "Name",
                keyValue: "Admin"
            );
        }
    }
}
