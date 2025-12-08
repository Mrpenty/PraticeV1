using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticeV1.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class int4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatusOder",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 8, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4335), "List of moview ", "Movie", new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4337) },
                    { 9, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4338), "Various kinds of tools", "Tools", new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4338) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "QuantityInStock", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 16, 8, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4485), "Moview about a boy", "Beatifullboy", 699.99m, 50, 0, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4485) },
                    { 17, 9, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4487), "A durable tool for construction tasks", "Hammer", 19.99m, 50, 0, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4488) },
                    { 18, 8, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4489), "A thrilling sci-fi adventure", "Science Fiction Novel", 19.99m, 100, 0, new DateTime(2025, 12, 8, 4, 56, 33, 123, DateTimeKind.Utc).AddTicks(4489) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusOder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 4, 2, 3, 18, 87, DateTimeKind.Utc).AddTicks(8358), "Electronic gadgets and devices", "Electronics", new DateTime(2025, 12, 4, 2, 3, 18, 87, DateTimeKind.Utc).AddTicks(8363) },
                    { 2, new DateTime(2025, 12, 4, 2, 3, 18, 87, DateTimeKind.Utc).AddTicks(8366), "Various kinds of books", "Books", new DateTime(2025, 12, 4, 2, 3, 18, 87, DateTimeKind.Utc).AddTicks(8366) }
                });
        }
    }
}
