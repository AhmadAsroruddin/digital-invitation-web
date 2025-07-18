using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnEventToSubEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestlistConfigs_Events_EventId",
                table: "GuestlistConfigs");

            migrationBuilder.DropIndex(
                name: "IX_RSVPs_GuestSubEventId",
                table: "RSVPs");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "GuestlistConfigs",
                newName: "SubEventId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestlistConfigs_EventId",
                table: "GuestlistConfigs",
                newName: "IX_GuestlistConfigs_SubEventId");

            migrationBuilder.CreateIndex(
                name: "IX_RSVPs_GuestSubEventId",
                table: "RSVPs",
                column: "GuestSubEventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestlistConfigs_SubEvents_SubEventId",
                table: "GuestlistConfigs",
                column: "SubEventId",
                principalTable: "SubEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestlistConfigs_SubEvents_SubEventId",
                table: "GuestlistConfigs");

            migrationBuilder.DropIndex(
                name: "IX_RSVPs_GuestSubEventId",
                table: "RSVPs");

            migrationBuilder.RenameColumn(
                name: "SubEventId",
                table: "GuestlistConfigs",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestlistConfigs_SubEventId",
                table: "GuestlistConfigs",
                newName: "IX_GuestlistConfigs_EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RSVPs_GuestSubEventId",
                table: "RSVPs",
                column: "GuestSubEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestlistConfigs_Events_EventId",
                table: "GuestlistConfigs",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
