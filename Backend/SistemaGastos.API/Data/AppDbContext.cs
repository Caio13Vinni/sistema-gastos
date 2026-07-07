using SistemaGastos.API.Models;
namespace SistemaGastos.API.Data;

using Microsoft.EntityFrameworkCore;

// Conexão com o banco (SQLite). Guarda pessoas e transações em um arquivo local.
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();

    public DbSet<Transacao> Transacoes => Set<Transacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.HasIndex(t => t.PessoaId);
            entity.HasIndex(t => t.DataDeCriacao);
            entity.HasIndex(t => new { t.PessoaId, t.DataDeCriacao });

            entity.Property(t => t.Descricao).IsRequired().HasMaxLength(500);
            entity.Property(t => t.Valor).HasColumnType("decimal(18,2)");
            entity.Property(t => t.Tipo).HasConversion<int>();

            entity.HasOne(t => t.Pessoa)
                  .WithMany(p => p.Transacoes)
                  .HasForeignKey(t => t.PessoaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}