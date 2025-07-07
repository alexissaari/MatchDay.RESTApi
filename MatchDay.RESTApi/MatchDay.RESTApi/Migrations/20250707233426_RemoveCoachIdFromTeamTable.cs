using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchDay.RESTApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoachIdFromTeamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Teams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Teams",
                type: "INTEGER",
                nullable: true);
        }
    }
}
