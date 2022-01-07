namespace ECommerceApp.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Pedido");
            AlterColumn("dbo.Pedido", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Pedido", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Pedido");
            AlterColumn("dbo.Pedido", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Pedido", "ID");
        }
    }
}
