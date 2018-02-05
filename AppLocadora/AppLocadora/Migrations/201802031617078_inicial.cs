namespace AppLocadora.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filmes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sinopse = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Generos",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nome = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmeGeneros",
                c => new
                    {
                        Filme_Id = c.Int(nullable: false),
                        Genero_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Filme_Id, t.Genero_Id })
                .ForeignKey("dbo.Filmes", t => t.Filme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Generos", t => t.Genero_Id, cascadeDelete: true)
                .Index(t => t.Filme_Id)
                .Index(t => t.Genero_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilmeGeneros", "Genero_Id", "dbo.Generos");
            DropForeignKey("dbo.FilmeGeneros", "Filme_Id", "dbo.Filmes");
            DropIndex("dbo.FilmeGeneros", new[] { "Genero_Id" });
            DropIndex("dbo.FilmeGeneros", new[] { "Filme_Id" });
            DropTable("dbo.FilmeGeneros");
            DropTable("dbo.Generos");
            DropTable("dbo.Filmes");
        }
    }
}
