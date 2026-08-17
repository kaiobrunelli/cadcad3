namespace ControleAnaliseDesembolso.Modelos;

/// <summary>Dados da Ficha de Previsão de Desembolso (FPD-AF).</summary>
public class PreenchimentoFpd
{
    // ── Cabeçalho ──
    public string    Solicitante  { get; set; } = "";   // matrícula (usuário logado)
    public string    Gigov        { get; set; } = "";
    public string    Gestor       { get; set; } = "";   // matrícula
    public string    IdFpd        { get; set; } = "";
    public string    NumeroFpd    { get; set; } = "";
    public string    ContratoAf   { get; set; } = "";
    public DateTime? DataSolicitado { get; set; } = DateTime.Today;

    // Primeiro desembolso / Último desembolso / Adiantamento são mutuamente
    // exclusivos — nunca faz sentido uma FPD ser mais de um ao mesmo tempo.
    // "primeiro" | "ultimo" | "adiantamento" | null (nenhum = desembolso
    // normal, no meio do cronograma).
    public string? OpcaoExclusiva { get; set; }
    public bool PrimeiroDesembolso => OpcaoExclusiva == "primeiro";
    public bool UltimoDesembolsoSelecionado => OpcaoExclusiva == "ultimo";

    // ── Agentes ──
    public string AgenteFinanceiro     { get; set; } = "";
    public string CnpjAgenteFinanceiro { get; set; } = "";
    public string Tomador              { get; set; } = "";
    public string CnpjTomador          { get; set; } = "";
    public string AgenteTecnico        { get; set; } = "";
    public string CnpjAgenteTecnico    { get; set; } = "";
    public string AgentePromotor       { get; set; } = "";
    public string CnpjAgentePromotor   { get; set; } = "";
    public string Programa             { get; set; } = "";

    // ── Sim/Não (sim | nao | nsa | null) ──
    public string? Funcionalidade      { get; set; }
    public string? Conclusao           { get; set; }
    public string? TomadorAdimplente   { get; set; }
    public string? PromotorAdimplente  { get; set; }
    public string? RetornoParcial      { get; set; }
    public string? PlacaLocal          { get; set; }
    public string? LicencaInstalacao   { get; set; }
    public string? LicencaOperacao     { get; set; }
    public string? Excepcionalizacao   { get; set; }
    public string? CpAlterada          { get; set; }
    public string? Amortizacao         { get; set; }

    // Marcado → o desembolso entra no filtro "Agendado" da Home em vez de
    // Aguardando/Analisar (ver ControleAnaliseDesembolsoService.DataAgendamento
    // no servidor). Não é um item de validação do checklist de análise — é uma
    // propriedade bool da ficha, respondida no Cabeçalho (etapa 1). Não marcar = falso.
    public bool Sanepar { get; set; }

    // ── Obra / datas ──
    public DateTime? DataEmissaoEng        { get; set; }
    public string    SituacaoObra          { get; set; } = "Normal";
    public DateTime? DataEmissaoSocioAmb   { get; set; }
    public bool      Nsa                   { get; set; }
    public decimal?  PercObra              { get; set; }
    // normal | adiantamento — derivado de OpcaoExclusiva, não editável direto.
    public string TipoDesembolso => OpcaoExclusiva == "adiantamento" ? "adiantamento" : "normal";

    public string    InssObs     { get; set; } = "";

    // ── Financeiro ──
    public decimal?  SolicitadoVi     { get; set; }
    public decimal?  GlosadoVi        { get; set; }
    public decimal?  AceitoVi         { get; set; }
    public decimal?  ParticipacaoFgts { get; set; }   // valor FGTS (parcela)
    public decimal?  Contrapartida    { get; set; }
    public decimal?  Ve               { get; set; }   // Valor do Empréstimo
    public decimal?  CpAtual          { get; set; }
    public decimal?  Desembolsado     { get; set; }
    public decimal?  Integralizado    { get; set; }
    public decimal?  ParcelaFgts      { get; set; }
    public decimal?  Integralizar     { get; set; }
    public decimal?  SaldoDesembolsar { get; set; }
    public decimal?  SaldoIntegralizar{ get; set; }

    // ── Percentuais calculados (exibidos na tabela quando há valores) ──
    // FGTS: quanto o desembolso FGTS representa do VE
    public decimal? PercFgts => Ve is > 0 && ParticipacaoFgts is not null
        ? Math.Round((ParticipacaoFgts.Value / Ve.Value) * 100, 3) : null;
    // Contrapartida: quanto a contrapartida representa do FGTS
    public decimal? PercContrapartida => ParticipacaoFgts is > 0 && Contrapartida is not null
        ? Math.Round((Contrapartida.Value / ParticipacaoFgts.Value) * 100, 3) : null;
    // Global: (FGTS + contrapartida) sobre o VE
    public decimal? PercGlobal => Ve is > 0 && ParticipacaoFgts is not null
        ? Math.Round(((ParticipacaoFgts.Value + (Contrapartida ?? 0)) / Ve.Value) * 100, 3) : null;

    public bool TemValoresFinanceiros => Ve is > 0 && ParticipacaoFgts is > 0;
}
