using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Students.Migrations
{
    /// <inheritdoc />
    public partial class AddRepresentativeAndClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_REPRESENTATIVE",
                columns: table => new
                {
                    REPRESENTATIVE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    REPRESENTATIVE_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REPRESENTATIVE", x => x.REPRESENTATIVE_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CLIENT",
                columns: table => new
                {
                    CLIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FIRST_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LAST_NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BIRTH_DATE = table.Column<DateTime>(type: "date", nullable: false),
                    OBSERVATION = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    REPRESENTATIVE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CLIENT", x => x.CLIENT_ID);
                    table.ForeignKey(
                        name: "FK_TB_CLIENT_TB_REPRESENTATIVE_REPRESENTATIVE_ID",
                        column: x => x.REPRESENTATIVE_ID,
                        principalTable: "TB_REPRESENTATIVE",
                        principalColumn: "REPRESENTATIVE_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CLIENT_REPRESENTATIVE_ID",
                table: "TB_CLIENT",
                column: "REPRESENTATIVE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_REPRESENTATIVE_CPF",
                table: "TB_REPRESENTATIVE",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CLIENT");

            migrationBuilder.DropTable(
                name: "TB_REPRESENTATIVE");
        }
    }
}
