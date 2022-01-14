using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceApp.Infra.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CupomDesconto",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCupom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FimVigencia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupomDesconto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Altura = table.Column<double>(type: "float", nullable: false),
                    Largura = table.Column<double>(type: "float", nullable: false),
                    Profundidade = table.Column<double>(type: "float", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CupomDescontoID = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    ValorFrete = table.Column<double>(type: "float", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    CodigoPedido = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pedido_CupomDesconto_CupomDescontoID",
                        column: x => x.CupomDescontoID,
                        principalTable: "CupomDesconto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoPedido",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoID = table.Column<int>(type: "int", nullable: false),
                    ProdutoID = table.Column<int>(type: "int", nullable: false),
                    ValorProduto = table.Column<double>(type: "float", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPedido", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Pedido_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedido",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Produto_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_CupomDescontoID",
                table: "Pedido",
                column: "CupomDescontoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_PedidoID",
                table: "ProdutoPedido",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_ProdutoID",
                table: "ProdutoPedido",
                column: "ProdutoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoPedido");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "CupomDesconto");
        }
    }
}
