using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaNotificacao.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaNotificacao.Infra.EntityConfig
{
    public class NotificacaoConfig : IEntityTypeConfiguration<Notificacao>
    {

        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {

            builder.ToTable("PLA_NOT_TB001_NOTIFICACAO");

            builder.HasKey(x => x.CodigoNotificacao);

            builder.Property(x => x.CodigoNotificacao)
                .HasColumnName("CO_NOTIFICACAO");

            // Nullable de propósito: EnviarGeralAsync/EnviarCoordenacaoAsync (via
            // EnviarIndividualAsync) mandam codigoAplicativo=null quando a
            // notificação não é de um módulo específico — o tipo no domínio já é
            // CodigoAplicativo? (nullable), o banco tinha ficado mais restrito.
            builder.Property(x => x.CodigoAplicativo)
                .HasColumnName("CO_APLICATIVO");

            builder.Property(x => x.CodigoUsuarioEmissor)
                .HasColumnName("CO_USUARIO_EMISSOR")
                .HasMaxLength(7);

            builder.Property(x => x.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Mensagem)
                .HasColumnName("MENSAGEM")
                .HasMaxLength(1000)
                .IsRequired();

            // REVERTIDO para int puro: a coluna TIPO no banco ainda é numérica —
            // .HasConversion<string>() exigiria migration (mudar a coluna pra
            // varchar) que NÃO foi aplicada, e causava SqlException em todo
            // SaveChangesAsync (ex.: ao notificar um novo comentário), com o
            // efeito colateral de o comentário em si já ter sido salvo em outro
            // DbContext antes do erro estourar — sintoma: front mostra sucesso
            // (e o registro existe no banco) mas a API retorna erro.
            //
            // TODO (pendente, não crítico agora): o enum TipoNotificacao foi
            // reordenado para bater com Plataforma.UI.Shared.Enum.TipoNotificacao.
            // Enquanto a coluna for int, notificações GRAVADAS ANTES da
            // reordenação podem ser lidas com o Tipo trocado (ex.: uma antiga
            // "Alerta" pode aparecer como "Normal"). Para corrigir de vez:
            // 1) migration alterando TIPO para varchar(20);
            // 2) backfill mapeando o int antigo pro nome certo;
            // 3) só então reintroduzir .HasConversion<string>() aqui.
            builder.Property(x => x.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .HasColumnName("DT_CRIACAO")
                .IsRequired();

            builder.Property(x => x.DataValidade)
                .HasColumnName("DT_VALIDADE");

        }
    }

    public class NotificacaoUsuario
    {

    }
}