using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain;

namespace Pedidos.Data.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
			builder.HasKey(p => p.Id);
			builder.Property(p => p.IniciadoEm).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
			builder.Property(p => p.Status).HasConversion<string>();
			builder.Property(p => p.TipoFrete).HasConversion<int>();
			builder.Property(p => p.Obersavao).HasColumnType("varchar(512");
			builder.HasMany(p => p.Itens).WithOne(p => p.Pedido).OnDelete(DeleteBehavior.Cascade);
        }
    }
}