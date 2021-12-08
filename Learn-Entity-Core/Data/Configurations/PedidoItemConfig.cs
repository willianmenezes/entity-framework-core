using Learn_Entity_Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn_Entity_Core.Data.Configurations
{
    public class PedidoItemConfig : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Quantidade).HasDefaultValue(1).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Desconto).IsRequired();
        }
    }
}
