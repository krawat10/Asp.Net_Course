namespace ASPMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductCountry : DbMigration
    {
        /// <summary>
        /// Co się dzieje po 'update'
        /// </summary>
        public override void Up()
        {
            AddColumn("dbo.Products", "Country", c => c.String());
        }

        /// <summary>
        /// Co się dzieje po 'cofnij' (Update-Database -TargetMigration:"{Wcześniejsza nazwa migracji}")
        /// </summary>
        public override void Down()
        {
            DropColumn("dbo.Products", "Country");
        }
    }
}
