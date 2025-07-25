﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnSubEventInRSVP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_SubEvents_SubEventId",
                table: "RSVPs");

            migrationBuilder.AlterColumn<int>(
                name: "SubEventId",
                table: "RSVPs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_SubEvents_SubEventId",
                table: "RSVPs",
                column: "SubEventId",
                principalTable: "SubEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_SubEvents_SubEventId",
                table: "RSVPs");

            migrationBuilder.AlterColumn<int>(
                name: "SubEventId",
                table: "RSVPs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_SubEvents_SubEventId",
                table: "RSVPs",
                column: "SubEventId",
                principalTable: "SubEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
