using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCatechismEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_catechisms_category",
                table: "catechisms");

            migrationBuilder.DropColumn(
                name: "category",
                table: "catechisms");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "catechisms",
                newName: "book_name");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "catechisms",
                newName: "description");

            migrationBuilder.RenameIndex(
                name: "ix_catechisms_name",
                table: "catechisms",
                newName: "ix_catechisms_book_name");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "catechisms",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "catechisms",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "author_name",
                table: "catechisms",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_catechisms_author_name",
                table: "catechisms",
                column: "author_name");

            migrationBuilder.CreateIndex(
                name: "ix_catechisms_tags",
                table: "catechisms",
                column: "tags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_catechisms_author_name",
                table: "catechisms");

            migrationBuilder.DropIndex(
                name: "ix_catechisms_tags",
                table: "catechisms");

            migrationBuilder.DropColumn(
                name: "author_name",
                table: "catechisms");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "catechisms",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "book_name",
                table: "catechisms",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "ix_catechisms_book_name",
                table: "catechisms",
                newName: "ix_catechisms_name");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "updated_at",
                table: "catechisms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "created_at",
                table: "catechisms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "catechisms",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_catechisms_category",
                table: "catechisms",
                column: "category");
        }
    }
}
