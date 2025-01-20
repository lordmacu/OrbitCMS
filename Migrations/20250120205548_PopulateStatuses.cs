using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms.Migrations
{
    /// <inheritdoc />
    public partial class PopulateStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
            { Guid.NewGuid(), "Draft" },
            { Guid.NewGuid(), "Published" },
            { Guid.NewGuid(), "Archived" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Statuses WHERE Name IN ('Draft', 'Published', 'Archived')");
        }
    }
}
