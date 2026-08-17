using ControleAnaliseDesembolso.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleAnaliseDesembolso.Infra.EntityContext;

public class DesembolsoEntradaConfig : IEntityTypeConfiguration<DesembolsoEntrada>
{
    public void Configure(EntityTypeBuilder<DesembolsoEntrada> builder)
    {
        builder.ToTable("CAD_TB001_DESEMBOLSO_ENTRADA");

        builder.HasKey(x => x.Id)
            .HasName("PK_CAD_TB001_DESEMBOLSO_ENTRADA");

        builder.Property(x => x.Id)
            .HasColumnName("ID")
            .IsRequired();

        builder.Property(x => x.IdFpdGegov)
            .HasColumnName("ID_FPD_GEGOV")
            .IsRequired();

        builder.Property(x => x.CoGigov)
            .HasColumnName("CO_GIGOV")
            .HasMaxLength(4)
            .IsFixedLength();

        builder.Property(x => x.CoFpd)
            .HasColumnName("CO_FPD");

        builder.Property(x => x.DeTomador)
            .HasColumnName("DE_TOMADOR")
            .HasMaxLength(255);

        builder.Property(x => x.CoTomador)
            .HasColumnName("CO_TOMADOR")
            .HasMaxLength(14)
            .IsFixedLength();

        builder.Property(x => x.DeOperacional)
            .HasColumnName("DE_OPERACIONAL")
            .HasMaxLength(255);

        builder.Property(x => x.CoOperacional)
            .HasColumnName("CO_OPERACIONAL")
            .HasMaxLength(14)
            .IsFixedLength();

        builder.Property(x => x.DeAgentePromotor)
            .HasColumnName("DE_AGENTE_PROMOTOR")
            .HasMaxLength(255);

        builder.Property(x => x.CoAgentePromotor)
            .HasColumnName("CO_AGENTE_PROMOTOR")
            .HasMaxLength(14)
            .IsFixedLength();

        builder.Property(x => x.ContratoAf)
            .HasColumnName("CONTRATO_AF")
            .HasMaxLength(7)
            .IsFixedLength();

        builder.Property(x => x.DvAf)
            .HasColumnName("DV_AF")
            .HasMaxLength(4)
            .IsFixedLength();

        builder.Property(x => x.ContratoAo)
            .HasColumnName("CONTRATO_AO")
            .HasMaxLength(7)
            .IsFixedLength();

        builder.Property(x => x.DtEngenharia)
            .HasColumnName("DT_ENGENHARIA")
            .HasColumnType("datetime");

        builder.Property(x => x.SituacaoObra)
            .HasColumnName("SITUACAOOBRA")
            .HasMaxLength(60);

        builder.Property(x => x.PercentualObra)
            .HasColumnName("PERCENTUAL_OBRA");
    }
}
