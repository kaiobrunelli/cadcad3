using ControleAnaliseDesembolso.Domain.Enums;

namespace ControleAnaliseDesembolso.Application.Dtos.Response
{
    // Resposta combinada pro dialog de detalhes da Home — ficha + checklist
    // (com status) + comentários de cada item do checklist, tudo numa
    // chamada só. Formato próprio deste módulo: não tenta imitar o
    // DetalheContrato/Validacao mockados no client (aqueles têm campos como
    // SubItens, Fase, Amortizacao que não existem em nenhuma tabela hoje).
    public class DesembolsoDetalheResponse
    {
        public int CoDesembolso { get; set; }
        public int CoFpd { get; set; }
        public TipoStatusDesembolso Status { get; set; }
        public DateTime DtSolicitado { get; set; }
        public DateTime DtPrazo { get; set; }
        public DateTime? DtConclusao { get; set; }
        public string? ResponsavelAnalise { get; set; }
        public string? ResponsavelBaixa { get; set; }

        public string CoContratoAf { get; set; } = string.Empty;
        public string CoContratoAfDv { get; set; } = string.Empty;
        public string CoGigov { get; set; } = string.Empty;
        public string MutuarioFinal { get; set; } = string.Empty;
        public string CnpjMutuarioFinal { get; set; } = string.Empty;
        public string AgenteFinanceiro { get; set; } = string.Empty;
        public string AgentePromotor { get; set; } = string.Empty;
        public string Programa { get; set; } = string.Empty;
        public string TipoDesembolso { get; set; } = string.Empty;
        public bool PrimeiroDesembolso { get; set; }
        public bool UltimoDesembolso { get; set; }
        public decimal PercentualObra { get; set; }
        public decimal ValorEmprestimo { get; set; }
        public decimal SolicitadoVi { get; set; }
        public decimal ParticipacaoFgts { get; set; }
        public decimal Contrapartida { get; set; }

        // Restante dos campos da FPD — não aparecem na tela de detalhes, mas
        // são necessários pra reenviar (PUT .../reenviar) sem perder dado que
        // não passou pelo formulário de edição (o servidor sobrescreve TODOS
        // os campos da ficha a partir do request, então o reenvio precisa
        // conhecer os que não foram editados também).
        public string MatriculaSolicitante { get; set; } = string.Empty;
        public string MatriculaGestor { get; set; } = string.Empty;
        public string CnpjAf { get; set; } = string.Empty;
        public string? AgenteTecnicoOperador { get; set; }
        public string? CnpjAgenteTecnicoOperador { get; set; }
        public string CnpjAgentePromotor { get; set; } = string.Empty;
        public DateTime DtEngenharia { get; set; }
        public string? SituacaoObra { get; set; }
        public DateTime? DtSocioAmbiental { get; set; }
        public DateTime? Concluido { get; set; }
        public decimal GlossadoVi { get; set; }
        public decimal AceitoVi { get; set; }
        public decimal Desembolsado { get; set; }
        public decimal SaldoADesembolsar { get; set; }
        public bool? Excepcionalizado { get; set; }
        public decimal ContrapartidaAtual { get; set; }
        public decimal Integralizado { get; set; }
        public decimal SaldoAIntegralizar { get; set; }
        public bool? ContrapartidaAlterada { get; set; }
        public bool? Amortizacao { get; set; }
        public bool? Sanepar { get; set; }
        public bool? RetornoParcial { get; set; }
        public bool? PlacaLocal { get; set; }
        public bool? LicensaInstalacao { get; set; }
        public bool? LicensaOperacao { get; set; }
        public bool? Funcionalidade { get; set; }

        public List<ChecklistItemResponse> Checklist { get; set; } = new();

        // Comentários cujo CoValidacao não bate com nenhum item do checklist
        // ativo do desembolso (ex.: a justificativa de "Pedido Negado", que é
        // um item desativado de propósito — nunca vira ValidacaoDesembolso,
        // só existe como âncora no catálogo). Sem isso, esse comentário nunca
        // apareceria em lugar nenhum da resposta.
        public List<ComentarioValidacaoResponse> ComentariosGerais { get; set; } = new();
    }
}
