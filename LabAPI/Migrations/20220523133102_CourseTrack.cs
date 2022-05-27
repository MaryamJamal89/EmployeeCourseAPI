using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabAPI.Migrations
{
    public partial class CourseTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrackId",
                table: "Courses",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Tracks_TrackId",
                table: "Courses",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Tracks_TrackId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TrackId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Courses");
        }
    }
}
