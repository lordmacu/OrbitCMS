using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms.Migrations
{
    /// <inheritdoc />
    public partial class PopulatePostTypes : Migration
    {
   protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PostTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "post" },
                    { Guid.NewGuid(), "page" },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PostTypes WHERE Name IN ('post', 'page'");
        }
    }
}
