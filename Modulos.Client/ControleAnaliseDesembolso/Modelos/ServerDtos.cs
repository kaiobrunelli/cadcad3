namespace ControleAnaliseDesembolso.Modelos;

// Espelham o formato JSON dos DTOs reais da API (ControleAnaliseDesembolso.Api).
// Client e servidor não compartilham assembly, então esses tipos existem só
// pra desserializar a resposta HTTP — o matching é por nome de propriedade
// (case-insensitive, padrão do System.Net.Http.Json). Ints/enums sem
// [JsonConverter] no servidor (Status, Situacao) vêm como número; os que têm
// (TipoRegistro) vêm como string camelCase.

public class DesembolsoResponseDto
{
    public int CoDesembolso { get; set; }
    public string Id { get; set; } = "";
    public string NumId { get; set; } = "";
    public DateTime DtSolicitado { get; set; }
    public string Contrato { get; set; } = "";
    public string Mutuario { get; set; } = "";
    public string Gigov { get; set; } = "";
    public decimal Valor { get; set; }
    public int ValidacoesOk { get; set; }
    public int ValidacoesTotal { get; set; }
    public int Status { get; set; } // TipoStatusDesembolso: 0 Validar, 1 Analisar, 2 BaixarDRP, 3 Finalizado, 4 Negado
    public bool PrimeiroDesembolso { get; set; }
    public bool Adiantamento { get; set; }
    public bool UltimoDesembolso { get; set; }
    public DateTime PrazoFinal { get; set; }
    public string? ResponsavelAnalise { get; set; }
    public bool DataAgendamento { get; set; }
    public DateTime? DtConclusao { get; set; }
}

public class ChecklistItemDto
{
    public int CoValidacao { get; set; }
    public string? DeValidacao { get; set; }
    public int Situacao { get; set; } // TipoSituacaoValidacao: 0 Analisar, 1 Aprovado, 2 Negado
    public List<ComentarioValidacaoDto> Comentarios { get; set; } = [];
}

public class ComentarioValidacaoDto
{
    public int CoRegistroValidacao { get; set; }
    public int CoValidacao { get; set; }
    public string? Texto { get; set; }
    public string TipoRegistro { get; set; } = ""; // justificativa | informativo | parecer | observacao | rejeicao
    public string? MatriculaAutor { get; set; }
    public string? NomeAutor { get; set; }
    public int UnidadeAutor { get; set; }
    public string Sigla { get; set; } = "";
    public DateTime DtCriacao { get; set; }
}

public class ValidacaoTemplateDto
{
    public int CoValidacao { get; set; }
    public string? DeValidacao { get; set; }
}

public class DesembolsoDetalheDto
{
    public int CoDesembolso { get; set; }
    public int CoFpd { get; set; }
    public int Status { get; set; }
    public DateTime DtSolicitado { get; set; }
    public DateTime DtPrazo { get; set; }
    public DateTime? DtConclusao { get; set; }
    public string? ResponsavelAnalise { get; set; }
    public string? ResponsavelBaixa { get; set; }
    public string CoContratoAf { get; set; } = "";
    public string CoContratoAfDv { get; set; } = "";
    public string CoGigov { get; set; } = "";
    public string MutuarioFinal { get; set; } = "";
    public string CnpjMutuarioFinal { get; set; } = "";
    public string AgenteFinanceiro { get; set; } = "";
    public string AgentePromotor { get; set; } = "";
    public string Programa { get; set; } = "";
    public string TipoDesembolso { get; set; } = "";
    public bool PrimeiroDesembolso { get; set; }
    public bool UltimoDesembolso { get; set; }
    public decimal PercentualObra { get; set; }
    public decimal ValorEmprestimo { get; set; }
    public decimal SolicitadoVi { get; set; }
    public decimal ParticipacaoFgts { get; set; }
    public decimal Contrapartida { get; set; }

    public string MatriculaSolicitante { get; set; } = "";
    public string MatriculaGestor { get; set; } = "";
    public string CnpjAf { get; set; } = "";
    public string? AgenteTecnicoOperador { get; set; }
    public string? CnpjAgenteTecnicoOperador { get; set; }
    public string CnpjAgentePromotor { get; set; } = "";
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

    public List<ChecklistItemDto> Checklist { get; set; } = [];
    public List<ComentarioValidacaoDto> ComentariosGerais { get; set; } = [];
}
