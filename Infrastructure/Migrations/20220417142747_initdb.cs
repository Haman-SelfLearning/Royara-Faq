using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Faqs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Updatetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Faqs",
                columns: new[] { "Id", "Answer", "CreateDate", "IsRemoved", "Question", "Updatetime" },
                values: new object[] { new Guid("7049e42c-9cae-42a3-a06d-5aca9c6e7cfb"), "Answer", new DateTime(2022, 4, 17, 18, 57, 46, 771, DateTimeKind.Local).AddTicks(5951), false, "Question", new DateTime(2022, 4, 17, 18, 57, 46, 771, DateTimeKind.Local).AddTicks(6050) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faqs");
        }
    }
}
