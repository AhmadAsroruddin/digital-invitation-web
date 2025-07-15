using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGuestSubEventIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_Guests_GeustId",
                table: "Checkins");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_SubEvents_SubEventId",
                table: "Checkins");

            migrationBuilder.DropIndex(
                name: "IX_Checkins_GeustId",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "GeustId",
                table: "Checkins");

            migrationBuilder.AlterColumn<int>(
                name: "SubEventId",
                table: "Checkins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Checkins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GuestSubEventId",
                table: "Checkins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_GuestId",
                table: "Checkins",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_GuestSubEventId",
                table: "Checkins",
                column: "GuestSubEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_GuestSubEvents_GuestSubEventId",
                table: "Checkins",
                column: "GuestSubEventId",
                principalTable: "GuestSubEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_Guests_GuestId",
                table: "Checkins",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_SubEvents_SubEventId",
                table: "Checkins",
                column: "SubEventId",
                principalTable: "SubEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_GuestSubEvents_GuestSubEventId",
                table: "Checkins");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_Guests_GuestId",
                table: "Checkins");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkins_SubEvents_SubEventId",
                table: "Checkins");

            migrationBuilder.DropIndex(
                name: "IX_Checkins_GuestId",
                table: "Checkins");

            migrationBuilder.DropIndex(
                name: "IX_Checkins_GuestSubEventId",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "GuestSubEventId",
                table: "Checkins");

            migrationBuilder.AlterColumn<int>(
                name: "SubEventId",
                table: "Checkins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Checkins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeustId",
                table: "Checkins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_GeustId",
                table: "Checkins",
                column: "GeustId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_Guests_GeustId",
                table: "Checkins",
                column: "GeustId",
                principalTable: "Guests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkins_SubEvents_SubEventId",
                table: "Checkins",
                column: "SubEventId",
                principalTable: "SubEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
