using ControleAnaliseDesembolso.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleAnaliseDesembolso.Infra.EntityContext;

public class ValidacaoDesembolsoConfig : IEntityTypeConfiguration<ValidacaoDesembolso>
{
    public void Configure(EntityTypeBuilder<ValidacaoDesembolso> builder)
    {
        builder.ToTable("CAD_TB005_VALIDACAO_DESEMBOLSO");

        builder.HasKey(x => new
        {
            x.CoValidacao,
            x.CoDesembolso
        });
        builder.Property(x => x.CoValidacao)
            .HasColumnName("CO_VALIDACAO")
            .IsRequired();

        builder.Property(x => x.CoDesembolso)
            .HasColumnName("CO_DESEMBOLSO")
            .IsRequired();

        builder.Property(x => x.DeValidacao)
            .HasColumnName("DE_VALIDACAO")
            .HasMaxLength(500);

        builder.Property(x => x.CampoVinculado)
            .HasColumnName("CAMPO_VINCULADO")
            .HasMaxLength(100);

        builder.Property(x => x.Situacao)
            .HasColumnName("SITUACAO")
            .HasConversion<int>();
    }
}
