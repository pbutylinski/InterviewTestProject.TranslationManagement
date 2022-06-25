using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationManagement.DataAccess.Migrations
{
    public partial class AddAssignedTranslator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedTranslatorId",
                table: "TranslationJobs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslationJobs_AssignedTranslatorId",
                table: "TranslationJobs",
                column: "AssignedTranslatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TranslationJobs_Translators_AssignedTranslatorId",
                table: "TranslationJobs",
                column: "AssignedTranslatorId",
                principalTable: "Translators",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslationJobs_Translators_AssignedTranslatorId",
                table: "TranslationJobs");

            migrationBuilder.DropIndex(
                name: "IX_TranslationJobs_AssignedTranslatorId",
                table: "TranslationJobs");

            migrationBuilder.DropColumn(
                name: "AssignedTranslatorId",
                table: "TranslationJobs");
        }
    }
}
