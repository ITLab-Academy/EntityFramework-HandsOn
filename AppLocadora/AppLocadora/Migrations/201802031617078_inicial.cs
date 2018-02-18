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
                        Categoria_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoriasFilme", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.CategoriasFilme",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 20, unicode: false),
                        Cor = c.String(nullable: false, maxLength: 7, fixedLength: true, unicode: false),
                        Preco = c.Decimal(nullable: false, precision: 10, scale: 2),
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
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 80, unicode: false),
                        Telefone = c.String(nullable: false, maxLength: 11, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocacaoEncargos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Referencia = c.String(maxLength: 200, unicode: false),
                        Locacao_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locacoes", t => t.Locacao_Id, cascadeDelete: true)
                .Index(t => t.Locacao_Id);
            
            CreateTable(
                "dbo.LocacoesFilmes",
                c => new
                    {
                        LocacaoId = c.Int(nullable: false),
                        FilmeId = c.Int(nullable: false),
                        Preco = c.Decimal(nullable: false, precision: 10, scale: 2),
                        DataDevolucao = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.LocacaoId, t.FilmeId })
                .ForeignKey("dbo.Filmes", t => t.FilmeId, cascadeDelete: true)
                .ForeignKey("dbo.Locacoes", t => t.LocacaoId, cascadeDelete: true)
                .Index(t => t.LocacaoId)
                .Index(t => t.FilmeId);
            
            CreateTable(
                "dbo.Locacoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataLocacao = c.DateTime(nullable: false),
                        DataDevolucaoPrevista = c.DateTime(nullable: false),
                        Cliente_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id, cascadeDelete: true)
                .Index(t => t.Cliente_Id);
            
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
            DropForeignKey("dbo.LocacoesFilmes", "LocacaoId", "dbo.Locacoes");
            DropForeignKey("dbo.LocacaoEncargos", "Locacao_Id", "dbo.Locacoes");
            DropForeignKey("dbo.Locacoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.LocacoesFilmes", "FilmeId", "dbo.Filmes");
            DropForeignKey("dbo.FilmeGeneros", "Genero_Id", "dbo.Generos");
            DropForeignKey("dbo.FilmeGeneros", "Filme_Id", "dbo.Filmes");
            DropForeignKey("dbo.Filmes", "Categoria_Id", "dbo.CategoriasFilme");
            DropIndex("dbo.FilmeGeneros", new[] { "Genero_Id" });
            DropIndex("dbo.FilmeGeneros", new[] { "Filme_Id" });
            DropIndex("dbo.Locacoes", new[] { "Cliente_Id" });
            DropIndex("dbo.LocacoesFilmes", new[] { "FilmeId" });
            DropIndex("dbo.LocacoesFilmes", new[] { "LocacaoId" });
            DropIndex("dbo.LocacaoEncargos", new[] { "Locacao_Id" });
            DropIndex("dbo.Filmes", new[] { "Categoria_Id" });
            DropTable("dbo.FilmeGeneros");
            DropTable("dbo.Locacoes");
            DropTable("dbo.LocacoesFilmes");
            DropTable("dbo.LocacaoEncargos");
            DropTable("dbo.Clientes");
            DropTable("dbo.Generos");
            DropTable("dbo.CategoriasFilme");
            DropTable("dbo.Filmes");
        }
    }
}
