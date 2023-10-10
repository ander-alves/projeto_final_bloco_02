﻿using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Model;

namespace projeto_final_bloco_02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Renomear tabelas
            modelBuilder.Entity<Produto>().ToTable("tb_produto");
            //modelBuilder.Entity<Categoria>().ToTable("tb_categoria");

            // Configurar o relacionamento entre Produto e Categoria
            /*modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produto)
                .HasForeignKey("CategoriaId")
                .OnDelete(DeleteBehavior.Cascade);

            // base.OnModelCreating(modelBuilder);*/
        }


        //Registra dbSet - Objeto responsavel por manupular a tabela
        public DbSet<Produto> Produtos { get; set; } = null!;
        // public DbSet<Categoria> Categoria { get; set; } = null!;

    }
}
