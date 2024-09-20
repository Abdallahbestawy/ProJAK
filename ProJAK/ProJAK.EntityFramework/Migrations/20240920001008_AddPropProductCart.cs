using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProJAK.EntityFramework.Migrations
{
    public partial class AddPropProductCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductCarts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39527e49-3edc-4ed1-8e55-b7715292bd08",
                column: "ConcurrencyStamp",
                value: "85367909-6b6f-4cd3-8a02-69e57624368b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4566b930-e1f9-4777-a945-7c4237260ed1",
                column: "ConcurrencyStamp",
                value: "03c612f6-f8b6-40d1-aef3-0fe2f0a00904");
        }
    }
}
