using ControleAnaliseDesembolso.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleAnaliseDesembolso.Infra.EntityContext;

public class CadastroValidacaoConfig : IEntityTypeConfiguration<CadastroValidacao>
{
    public void Configure(EntityTypeBuilder<CadastroValidacao> builder)
    {
        builder.ToTable("CAD_TB004_CADASTRO_VALIDACOES");

        builder.HasKey(x => x.CoValidacao)
            .HasName("PK_CAD_TB004_CADASTRO_VALIDACOES");

        builder.Property(x => x.CoValidacao)
            .HasColumnName("CO_VALIDACAO")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.DeValidacao)
            .HasColumnName("DE_VALIDACAO")
            .HasMaxLength(500);

        builder.Property(x => x.DtValidacao)
            .HasColumnName("DT_VALIDACAO")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.CampoVinculado)
            .HasColumnName("CAMPO_VINCULADO")
            .HasMaxLength(100);

        builder.Property(x => x.UsuarioExclusao)
            .HasColumnName("USUARIO_EXCLUSAO")
            .HasMaxLength(7);

        builder.Property(x => x.DtExclusao)
            .HasColumnName("DT_EXCLUSAO")
            .HasColumnType("datetime");

        builder.Property(x => x.Desativado)
            .HasColumnName("DESATIVADO")
            .IsRequired();

        builder.HasMany(x => x.ValidacoesDesembolso)
            .WithOne(x => x.CadastroValidacao)
            .HasForeignKey(x => x.CoValidacao)
            .HasConstraintName("FK_VALIDACAO_DESEMBOLSO_CADASTRO_VALIDACOES");
    }
}
