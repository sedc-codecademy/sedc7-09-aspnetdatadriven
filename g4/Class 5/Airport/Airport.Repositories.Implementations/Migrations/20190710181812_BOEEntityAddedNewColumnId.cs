using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Repositories.Implementations.Migrations
{
    public partial class BOEEntityAddedNewColumnId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BusinessObjectEmployee",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "BusinessObjectEmployee");
        }
    }
}
