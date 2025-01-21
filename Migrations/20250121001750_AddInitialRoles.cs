using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rols",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { Guid.NewGuid(), "Editor" },
            { Guid.NewGuid(), "User" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Rols WHERE Name IN ('Editor', 'User')");
        }
    }
}
