using System;
using ACC.WebApp.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACC.WebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20260302120000_AddUserNameChangeCooldown")]
    public partial class AddUserNameChangeCooldown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUserNameChangeUtc",
                schema: "acc_identity",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUserNameChangeUtc",
                schema: "acc_identity",
                table: "AspNetUsers");
        }
    }
}
