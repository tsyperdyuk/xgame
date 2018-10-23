using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xgame.Db.Migrations
{
    public partial class question : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "QuestionImage",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerImage",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "QuestionImage",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "AnswerImage",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
