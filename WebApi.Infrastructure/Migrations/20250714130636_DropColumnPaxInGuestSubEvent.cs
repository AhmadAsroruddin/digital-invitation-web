using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnPaxInGuestSubEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pax",
                table: "GuestSubEvents");

            migrationBuilder.RenameColumn(
                name: "GuestSubEventId",
                table: "GuestSubEvents",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GuestSubEvents",
                newName: "GuestSubEventId");

            migrationBuilder.AddColumn<int>(
                name: "Pax",
                table: "GuestSubEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
