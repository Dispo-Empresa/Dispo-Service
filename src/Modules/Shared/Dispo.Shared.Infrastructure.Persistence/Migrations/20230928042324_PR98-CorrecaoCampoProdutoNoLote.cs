using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PR98CorrecaoCampoProdutoNoLote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Batches_BatchId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BatchId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BatchId",
                table: "Products",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BatchId",
                table: "Products",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Batches_BatchId",
                table: "Products",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}