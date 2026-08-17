using ControleAnaliseDesembolso.Domain.Entitys;
using ControleAnaliseDesembolso.Infra.EntityContext;
using Microsoft.EntityFrameworkCore;

namespace ControleAnaliseDesembolso.Infra.Data.Context
{
    public class ControleAnaliseDesembolsoContext(DbContextOptions<ControleAnaliseDesembolsoContext> options) : DbContext(options)
    {
        public DbSet<CadastroValidacao> CadastroValidacoes { get; set; }
        public DbSet<Desembolso> Desembolsos { get; set; }
        public DbSet<DesembolsoEntrada> DesembolsoEntradas { get; set; }
        public DbSet<FichaPedidoDesembolso> FichaPedidoDesembolsos { get; set; }
        public DbSet<ValidacaoDesembolso> ValidacaoDesembolsos { get; set; }
        public DbSet<ValidacaoRegistro> RegistroValidacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new CadastroValidacaoConfig().Configure(builder.Entity<CadastroValidacao>());
            new DesembolsoConfig().Configure(builder.Entity<Desembolso>());
            new DesembolsoEntradaConfig().Configure(builder.Entity<DesembolsoEntrada>());
            new FichaPedidoDesembolsoConfig().Configure(builder.Entity<FichaPedidoDesembolso>());
            new ValidacaoDesembolsoConfig().Configure(builder.Entity<ValidacaoDesembolso>());
            new RegistroValidacaoConfig().Configure(builder.Entity<ValidacaoRegistro>());
        }
    }
}
