using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace cms.Migrations
{
    public partial class AddDefaultCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[]
                {
                    Guid.NewGuid(), "Default", "default"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE Name = 'Default'");
        }
    }
}
