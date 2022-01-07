namespace ECommerceApp.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CupomDesconto",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CodigoCupom = c.String(),
                        InicioVigencia = c.DateTime(nullable: false),
                        FimVigencia = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Subtotal = c.Double(nullable: false),
                        ValorFrete = c.Double(nullable: false),
                        ValorTotal = c.Double(nullable: false),
                        CodigoPedido = c.String(),
                        CupomDesconto_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CupomDesconto", t => t.CupomDesconto_ID)
                .Index(t => t.CupomDesconto_ID);
            
            CreateTable(
                "dbo.Produto",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Valor = c.Double(nullable: false),
                        Altura = c.Double(nullable: false),
                        Largura = c.Double(nullable: false),
                        Profundidade = c.Double(nullable: false),
                        Peso = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProdutoPedido",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PedidoID = c.Long(nullable: false),
                        ProdutoID = c.Int(nullable: false),
                        ValorProduto = c.Double(nullable: false),
                        Quantidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Produto", t => t.ProdutoID, cascadeDelete: true)
                .Index(t => t.ProdutoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutoPedido", "ProdutoID", "dbo.Produto");
            DropForeignKey("dbo.Pedido", "CupomDesconto_ID", "dbo.CupomDesconto");
            DropIndex("dbo.ProdutoPedido", new[] { "ProdutoID" });
            DropIndex("dbo.Pedido", new[] { "CupomDesconto_ID" });
            DropTable("dbo.ProdutoPedido");
            DropTable("dbo.Produto");
            DropTable("dbo.Pedido");
            DropTable("dbo.CupomDesconto");
        }
    }
}
