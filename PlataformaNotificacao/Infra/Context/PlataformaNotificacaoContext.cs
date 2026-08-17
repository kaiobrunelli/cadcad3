using Microsoft.EntityFrameworkCore;

using PlataformaNotificacao.Domain;
using PlataformaNotificacao.Infra.EntityConfig;


namespace PlataformaNotificacao.Infra.Context
{
    public class PlataformaNotificacaoContext : DbContext
    {
        public string? Chave { get; set; }
        public PlataformaNotificacaoContext(string chave)
        {
            Chave = chave;

        }

        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<ControleVisualizacao> ControleVisualizacoes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Chave);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            new NotificacaoConfig().Configure(builder.Entity<Notificacao>());
            new ControleVisualizacaoConfig().Configure(builder.Entity<ControleVisualizacao>());
        }
    }
}
