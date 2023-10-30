using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class added_paymentMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PaymentMethodId",
                table: "Purchases",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PaymentMethod_PaymentMethodId",
                table: "Purchases",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PaymentMethod_PaymentMethodId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PaymentMethodId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Purchases");
        }
    }
}
