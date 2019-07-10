using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Repositories.Implementations.Migrations
{
    public partial class AddedManyToManyBOToEmployeeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessObjects_Employees_ResponsibleEmployeeId",
                table: "BusinessObjects");

            migrationBuilder.DropIndex(
                name: "IX_BusinessObjects_ResponsibleEmployeeId",
                table: "BusinessObjects");

            migrationBuilder.DropColumn(
                name: "ResponsibleEmployeeId",
                table: "BusinessObjects");

            migrationBuilder.CreateTable(
                name: "BusinessObjectEmployee",
                columns: table => new
                {
                    BusinessObjectId = table.Column<int>(nullable: false),
                    ResponsibleEmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessObjectEmployee", x => new { x.BusinessObjectId, x.ResponsibleEmployeeId });
                    table.ForeignKey(
                        name: "FK_BusinessObjectEmployee_BusinessObjects_BusinessObjectId",
                        column: x => x.BusinessObjectId,
                        principalTable: "BusinessObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessObjectEmployee_Employees_ResponsibleEmployeeId",
                        column: x => x.ResponsibleEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessObjectEmployee_ResponsibleEmployeeId",
                table: "BusinessObjectEmployee",
                column: "ResponsibleEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessObjectEmployee");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleEmployeeId",
                table: "BusinessObjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessObjects_ResponsibleEmployeeId",
                table: "BusinessObjects",
                column: "ResponsibleEmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessObjects_Employees_ResponsibleEmployeeId",
                table: "BusinessObjects",
                column: "ResponsibleEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
