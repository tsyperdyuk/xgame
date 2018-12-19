using Microsoft.EntityFrameworkCore.Migrations;

namespace Xgame.Db.Migrations
{
    public partial class QuestionStatusFieldRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApproveStatus",
                table: "Questions",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Questions",
                newName: "ApproveStatus");
        }
    }
}
