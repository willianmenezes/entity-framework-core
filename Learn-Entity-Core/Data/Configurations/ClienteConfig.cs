using Learn_Entity_Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn_Entity_Core.Data.Configurations
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType("CHAR(11)");
            builder.Property(p => p.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired();
            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}
