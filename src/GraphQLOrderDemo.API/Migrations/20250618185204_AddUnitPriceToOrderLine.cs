﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQLOrderExample.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPriceToOrderLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderLines",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderLines");
        }
    }
}
