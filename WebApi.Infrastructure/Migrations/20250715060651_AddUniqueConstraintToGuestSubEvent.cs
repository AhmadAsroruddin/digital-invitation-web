using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToGuestSubEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GuestSubEvents_GuestId",
                table: "GuestSubEvents");

            migrationBuilder.CreateIndex(
                name: "IX_GuestSubEvents_GuestId_SubEventId",
                table: "GuestSubEvents",
                columns: new[] { "GuestId", "SubEventId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GuestSubEvents_GuestId_SubEventId",
                table: "GuestSubEvents");

            migrationBuilder.CreateIndex(
                name: "IX_GuestSubEvents_GuestId",
                table: "GuestSubEvents",
                column: "GuestId");
        }
    }
}
