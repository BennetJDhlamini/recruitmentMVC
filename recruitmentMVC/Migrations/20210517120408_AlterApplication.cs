using Microsoft.EntityFrameworkCore.Migrations;

namespace recruitmentMVC.Migrations
{
    public partial class AlterApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdPath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matricCertificatePath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "relevantDocsPath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transcriptPath",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "matricCertificatePath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "relevantDocsPath",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "transcriptPath",
                table: "Applications");
        }
    }
}
