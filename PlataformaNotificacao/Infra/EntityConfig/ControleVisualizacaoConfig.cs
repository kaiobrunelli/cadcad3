using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaNotificacao.Domain;

namespace PlataformaNotificacao.Infra.EntityConfig
{
    public class ControleVisualizacaoConfig : IEntityTypeConfiguration<ControleVisualizacao>
    {
        public void Configure(EntityTypeBuilder<ControleVisualizacao> builder)
        {
            builder.ToTable("PLA_NOT_TB002_CONTROLE_VISUALIZACAO");

            builder.HasKey(x => x.CodigoVisualizacao);

            builder.Property(x => x.CodigoVisualizacao)
                .HasColumnName("CO_VISUALIZACAO");

            builder.Property(x => x.CodigoNotificacao)
                .HasColumnName("CO_NOTIFICACAO")
                .IsRequired();

            builder.Property(x => x.CodigoUsuario)
                .HasColumnName("CO_USUARIO")
                .HasMaxLength(7)
                .IsRequired();

            builder.Property(x => x.DataVisualizacao)
                .HasColumnName("DT_VISUALIZACAO");

            builder.Property(x => x.Link)
                .HasColumnName("LINK")
                .HasMaxLength(500);

            builder.HasOne(x => x.Notificacao)
                .WithMany(x => x.Destinatarios)
                .HasForeignKey(x => x.CodigoNotificacao)
                .HasPrincipalKey(x => x.CodigoNotificacao);
        }
    }
}