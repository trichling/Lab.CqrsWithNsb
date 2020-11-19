using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab.CqrsWithNsb.Application.ReadModelBuilder.Migrations
{
    public partial class AccountCurrentBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountsWithBalance",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsWithBalance", x => x.AccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsWithBalance");
        }
    }
}
