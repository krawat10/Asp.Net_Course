namespace ASPMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        /// <summary>
        /// Co się dzieje po 'update'
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Category = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }

        /// <summary>
        /// Co się dzieje po 'cofnij'. Tutaj jest to pierwszy krok(inicjacja bazy),
        /// czyli po cofnięciu usuwamy baze danych.
        /// </summary>
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
