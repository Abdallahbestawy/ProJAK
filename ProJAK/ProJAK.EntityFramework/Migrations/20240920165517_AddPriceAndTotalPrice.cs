using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProJAK.EntityFramework.Migrations
{
    public partial class AddPriceAndTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OrderAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderedProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39527e49-3edc-4ed1-8e55-b7715292bd08",
                column: "ConcurrencyStamp",
                value: "2b9316ab-f1ef-4d91-8268-bb42eccc4967");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4566b930-e1f9-4777-a945-7c4237260ed1",
                column: "ConcurrencyStamp",
                value: "5c859b6c-59fc-449d-b545-f25097a2db78");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderedProducts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39527e49-3edc-4ed1-8e55-b7715292bd08",
                column: "ConcurrencyStamp",
                value: "95232690-ec4e-49a5-be1d-415794891c3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4566b930-e1f9-4777-a945-7c4237260ed1",
                column: "ConcurrencyStamp",
                value: "e427ae4c-f388-419b-a86f-cc0eef596c6d");
        }
    }
}
