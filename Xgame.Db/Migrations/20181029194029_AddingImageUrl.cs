using Microsoft.EntityFrameworkCore.Migrations;

namespace Xgame.Db.Migrations
{
    public partial class AddingImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionImage",
                table: "Questions",
                newName: "QuestionImageUrl");

            migrationBuilder.RenameColumn(
                name: "AnswerImage",
                table: "Questions",
                newName: "AnswerImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionImageUrl",
                table: "Questions",
                newName: "QuestionImage");

            migrationBuilder.RenameColumn(
                name: "AnswerImageUrl",
                table: "Questions",
                newName: "AnswerImage");
        }
    }
}
