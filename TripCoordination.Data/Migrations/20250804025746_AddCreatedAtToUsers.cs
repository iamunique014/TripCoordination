﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripCoordination.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedAt",
            //    table: "AspNetUsers",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");
        }
    }
}
