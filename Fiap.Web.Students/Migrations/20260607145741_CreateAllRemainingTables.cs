using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Web.Students.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllRemainingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_STORE",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STORE", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "TB_SUPPLIER",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SUPPLIER", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "TB_ORDER",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OrderDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StoreId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDER", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_TB_CLIENT_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_CLIENT",
                        principalColumn: "CLIENT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_TB_STORE_StoreId",
                        column: x => x.StoreId,
                        principalTable: "TB_STORE",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PRODUCT",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Price = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SupplierId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCT", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_TB_PRODUCT_TB_SUPPLIER_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "TB_SUPPLIER",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductModel", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProductModel_TB_ORDER_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TB_ORDER",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductModel_TB_PRODUCT_OrderId",
                        column: x => x.OrderId,
                        principalTable: "TB_PRODUCT",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductModel_ProductId",
                table: "OrderProductModel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORDER_ClientId",
                table: "TB_ORDER",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORDER_StoreId",
                table: "TB_ORDER",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUCT_SupplierId",
                table: "TB_PRODUCT",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductModel");

            migrationBuilder.DropTable(
                name: "TB_ORDER");

            migrationBuilder.DropTable(
                name: "TB_PRODUCT");

            migrationBuilder.DropTable(
                name: "TB_STORE");

            migrationBuilder.DropTable(
                name: "TB_SUPPLIER");
        }
    }
}
