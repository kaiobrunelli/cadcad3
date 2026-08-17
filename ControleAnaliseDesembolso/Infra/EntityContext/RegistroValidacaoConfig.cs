using ControleAnaliseDesembolso.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleAnaliseDesembolso.Infra.EntityContext;

public class RegistroValidacaoConfig : IEntityTypeConfiguration<ValidacaoRegistro>
{
    public void Configure(EntityTypeBuilder<ValidacaoRegistro> builder)
    {
        builder.ToTable("CAD_TB006_VALIDACAO_REGISTRO");

        builder.HasKey(x => x.CoRegistroValidacao)
            .HasName("PK_CAD_TB006_VALIDACAO_REGISTRO");

        builder.Property(x => x.CoRegistroValidacao)
            .HasColumnName("CO_REGISTRO_VALIDACAO")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.CoValidacao)
            .HasColumnName("CO_VALIDACAO")
            .IsRequired();

        builder.Property(x => x.CoDesembolso)
            .HasColumnName("CO_DESEMBOLSO")
            .IsRequired();

        builder.Property(x => x.DeRegistro)
            .HasColumnName("DE_REGISTRO")
            .HasMaxLength(1000);

        builder.Property(x => x.TipoRegistro)
            .HasColumnName("TIPO_REGISTRO")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CoUsuario)
            .HasColumnName("CO_USUARIO")
            .HasMaxLength(7);

        builder.Property(x => x.DeUsuario)
            .HasColumnName("DE_USUARIO")
            .HasMaxLength(255);

        builder.Property(x => x.UnidadeUsuario)
            .HasColumnName("UNIDADE_USUARIO")
            .IsRequired();

        builder.Property(x => x.DtCriacao)
            .HasColumnName("DT_CRIACAO")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.Ativo)
            .HasColumnName("ATIVO")
            .HasDefaultValue(true)
            .IsRequired();
    }
}
