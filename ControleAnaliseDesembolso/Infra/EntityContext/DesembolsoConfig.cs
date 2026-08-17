using ControleAnaliseDesembolso.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleAnaliseDesembolso.Infra.EntityContext;

public class DesembolsoConfig : IEntityTypeConfiguration<Desembolso>
{
    public void Configure(EntityTypeBuilder<Desembolso> builder)
    {
        builder.ToTable("CAD_TB003_DESEMBOLSO");

        builder.HasKey(x => x.CoDesembolso)
            .HasName("PK_CAD_TB003_DESEMBOLSO");

        builder.Property(x => x.CoDesembolso)
            .HasColumnName("CO_DESEMBOLSO")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.CoFpd)
            .HasColumnName("CO_FPD")
            .IsRequired();

        builder.Property(x => x.ResponsavelAnalise)
            .HasColumnName("RESPONSAVEL_ANALISE")
            .HasMaxLength(7);

        builder.Property(x => x.ResponsavelBaixa)
            .HasColumnName("RESPONSAVEL_BAIXA")
            .HasMaxLength(7);

        builder.Property(x => x.MatriculaBaixa)
            .HasColumnName("MATRICULA_BAIXA")
            .HasMaxLength(7);

        builder.Property(x => x.Gestor)
            .HasColumnName("GESTOR")
            .HasMaxLength(7);

        builder.Property(x => x.DtPrazo)
            .HasColumnName("DT_PRAZO")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.StatusDesembolso)
            .HasColumnName("SITUACAO_DESEMBOLSO")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.DtConclusao)
            .HasColumnName("DT_CONCLUSAO")
            .HasColumnType("date");

        builder.HasOne(x => x.FichaPedidoDesembolso)
            .WithOne(x => x.Desembolso)
            .HasForeignKey<Desembolso>(x => x.CoFpd)
            .HasConstraintName("FK_CAD_TB002_FICHA_PEDIDO_DESEMBOLSO");

        builder.HasMany(x => x.ValidacoesDesembolso)
            .WithOne(x => x.Desembolso)
            .HasForeignKey(x => x.CoDesembolso)
            .HasConstraintName("FK_VALIDACAO_DESEMBOLSO_DESEMBOLSO");
    }
}
