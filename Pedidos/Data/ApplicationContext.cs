using Microsoft.EntityFrameworkCore;
using Pedidos.Domain;

namespace Pedidos.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Pedidos;User ID=sa;Password=1q2w3e4r!@#$");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>(p => 
            {
                p.ToTable("Clientes");
                p.HasKey(p => p.Id);
                p.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
                p.Property(p => p.Telefone).HasColumnType("char(11)");
                p.Property(p => p.CEP).HasColumnType("char(8)").IsRequired();
                p.Property(p => p.Cidade).HasMaxLength(60).IsRequired();
                p.Property(p => p.Estado).HasColumnType("char(2)").IsRequired();

                p.HasIndex(p => p.Telefone).HasName("idx_cliente_telefone");
            });

            builder.Entity<Produto>(p => 
            {
                p.ToTable("Produtos");
                p.HasKey(p => p.Id);
                p.Property(p => p.CodigoBarras).HasColumnType("varchar(14)").IsRequired();
                p.Property(p => p.Descricao).HasColumnType("varchar(60");
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.TipoProduto).HasConversion<string>();
            });

            builder.Entity<Pedido>(p => 
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.IniciadoEm).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
                p.Property(p => p.Status).HasConversion<string>();
                p.Property(p => p.TipoFrete).HasConversion<int>();
                p.Property(p => p.Obersavao).HasColumnType("varchar(512");

                p.HasMany(p => p.Itens).WithOne(p => p.Pedido).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PedidoItem>(p =>
            {
                p.ToTable("PedidoItens");
                p.HasKey(p => p.Id);
                p.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
                p.Property(p => p.Valor).IsRequired();
                p.Property(p => p.Desconto).IsRequired();
            });
        }
    }
}