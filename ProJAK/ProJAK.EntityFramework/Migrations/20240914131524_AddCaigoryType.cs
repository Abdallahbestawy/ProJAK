using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProJAK.EntityFramework.Migrations
{
    public partial class AddCaigoryType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SubCategorieId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SubCategorieId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SubCategorieId",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "ManufacturerId",
                table: "products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CategorieType",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_ManufacturerId",
                table: "products",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Manufacturers_ManufacturerId",
                table: "products",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Manufacturers_ManufacturerId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ManufacturerId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategorieType",
                table: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategorieId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SubCategorieId",
                table: "Categories",
                column: "SubCategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SubCategorieId",
                table: "Categories",
                column: "SubCategorieId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
