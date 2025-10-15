using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheRememberer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class properhierarchyofkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscordDbID",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DiscordDbID",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
