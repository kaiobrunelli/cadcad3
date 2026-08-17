using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PlataformaOperacional.Model.MonitoramentoModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PlataformaOperacional.Pages.Monitoramento.EntradaDeDados;

public partial class EntradaDeDados
{
    private static readonly string[] RequiredHeaders =
        [
            "CONTA",
        "HISTORICO",
        "VALOR",
        "DATAEFETIVA",
        "ORIGEM"
        ];

    private static readonly HashSet<int> ContasPermitidas = [63, 669];

    private static readonly HashSet<string> OrigensPermitidas = [
        "SINAF", "APF/AIV", "FINME", "Manual", "SIMCF", "ACI/AIV", "APF/AVI", "SITRF", "FINFP", "Excel"
        ];

    private const string HistoricoPattern = @"^\d{1,3} - .+$";
    private const string ValorPattern = @"^-?\d{1,3}(?:\.\d{3})*,\d{2}$";
    private const string DatePatern = @"^(?:31\/(?:0[13578]|1[02])\/\d{2}|(?:29|30)\/(?:0[13-9]|1[0-2])\/\d{2}|(?:0[1-9]|1\d|2[0-8])\/(?:0[1-9]|1[0-2])\/\d{2}|29\/02\/(?:[02468][048]|[13579][26]))$";


    private static NumberFormatInfo numberFormat = new()
    {
        NegativeSign = "-",
        NumberDecimalSeparator = ",",
        NumberGroupSeparator = "."
    };

    [GeneratedRegex(HistoricoPattern)]
    private static partial Regex HistoricoRegex();

    [GeneratedRegex(ValorPattern)]
    private static partial Regex ValorRegex();

    [GeneratedRegex(DatePatern)]
    private static partial Regex DateRegex();

    public static async Task<EntradaDeDadosImportResult> LerArquivoAsync(
        IBrowserFile? arquivo,
        CancellationToken cancellationToken = default)
    {
        var result = new EntradaDeDadosImportResult();

        var arquivoContract = new Contract<Notification>()
            .Requires()
            .IsNotNull(arquivo, nameof(arquivo), "Arquivo não informado.")
            .IsGreaterThan(arquivo?.Size ?? 0, 0, nameof(arquivo), "O arquivo está vazio.");

        result.AddNotifications(arquivoContract);

        if (!result.IsValid || arquivo is null)
            return result;

        await using var stream = arquivo.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024, cancellationToken: cancellationToken);
        using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

        var cabecalho = await reader.ReadLineAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(cabecalho))
        {
            result.AddNotification(nameof(arquivo), "O arquivo não possui cabeçalho.");
            return result;
        }

        var colunas = cabecalho.Split(';', StringSplitOptions.TrimEntries);
        var indices = colunas
            .Select((coluna, indice) => new { Chave = NormalizarCabecalho(coluna), Indice = indice })
            .GroupBy(item => item.Chave)
            .ToDictionary(group => group.Key, group => group.First().Indice);

        foreach (var requiredHeader in RequiredHeaders)
        {
            if (!indices.ContainsKey(requiredHeader))
                result.AddNotification("cabecalho", $"Coluna obrigatória '{requiredHeader}' não encontrada.");
        }

        if (!result.IsValid)
            return result;

        var linhaNumero = 1;
        DateTime? dataEfetivaArquivo = null;
        int? contaArquivo = null;

        string? linha;

        while ((linha = await reader.ReadLineAsync(cancellationToken)) is not null)
        {

            linhaNumero++;

            if (string.IsNullOrWhiteSpace(linha))
                continue;

            var campos = linha.Split(';');

            var contaTexto = ObterCampo(campos, indices, "CONTA");
            var dataApropriacao = ObterCampo(campos, indices, "DATAAPROPRIACAO");
            var historico = ObterCampo(campos, indices, "HISTORICO");
            var valorTexto = ObterCampo(campos, indices, "VALOR");
            var dataEfetivaTexto = ObterCampo(campos, indices, "DATAEFETIVA");
            var origem = ObterCampo(campos, indices, "ORIGEM");
            var dataProcessamento = ObterCampo(campos, indices, "DATAPROCESSAMENTO");
            var observacao = ObterCampo(campos, indices, "OBSERVACAO");

            var contratoLinha = new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(contaTexto, $"Linha {linhaNumero} conta", "Conta é obrigatória.")
                .IsNotNullOrWhiteSpace(historico, $"Linha {linhaNumero} historico", "Histórico é obrigatório.")
                .IsNotNullOrWhiteSpace(valorTexto, $"Linha {linhaNumero} valor", "Valor é obrigatório.")
                .IsNotNullOrWhiteSpace(dataEfetivaTexto, $"Linha {linhaNumero} dataEfetiva", "Data efetiva é obrigatória.")
                .IsNotNullOrWhiteSpace(origem, $"Linha {linhaNumero} origem", "Origem é obrigatória.");

            result.AddNotifications(contratoLinha);

            if (!contratoLinha.IsValid)
                continue;

            if (!int.TryParse(contaTexto, out var conta))
            {
                result.AddNotification($"Linha {linhaNumero}", $"Conta inválida: '{contaTexto}'.");
                continue;
            }

            var validacoesContract = new Contract<Notification>()
                .Requires()
                .IsTrue(ContasPermitidas.Contains(conta),
                    $"linha {linhaNumero} conta",
                    $"Conta {conta} inválida. Valores permitidos: 63 e 669.")
                .Matches(historico, HistoricoPattern,
                    $"linha {linhaNumero} historico",
                    "Histórico deve iniciar com 1, 2 ou 3 dígitos, seguido de ' - ' e ao menos um caractere.")
                .Matches(valorTexto, ValorPattern,
                    $"linha {linhaNumero} historico",
                    $"Valor inválido: {valorTexto}")
                .Matches(dataEfetivaTexto, DatePatern,
                    $"linha {linhaNumero} data",
                    $"Data inválida: {dataEfetivaTexto}; formato esperado: dd\\mm\\aa")
                .IsTrue(OrigensPermitidas.Contains(origem),
                    $"linha {linhaNumero} origem",
                    $"Origem não cadastrada: {origem}");

            result.AddNotifications(validacoesContract);

            if (!validacoesContract.IsValid)
                continue;


            if (!decimal.TryParse(
                    valorTexto,
                    NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint,
                    numberFormat,
                    out var valor))

            {
                result.AddNotification($"Linha {linhaNumero}", $"Valor inválido: '{valorTexto}'.");
                continue;
            }

            if (!DateTime.TryParseExact(
                    dataEfetivaTexto,
                    "dd/MM/yy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dataEfetiva))
            {
                result.AddNotification($"Linha {linhaNumero}", $"Data efetiva inválida: '{dataEfetivaTexto}'. Formato esperado: dd/MM/yy.");
                continue;
            }

            if (dataEfetiva > DateTime.Today)
            {
                result.AddNotification($"Linha {linhaNumero}", $"Data efetiva não pode ser superior à data atual.");
                continue;
            }

            var FileKeyValuesContract = new Contract<Notification>()
               .Requires()
               .IsTrue(!contaArquivo.HasValue || conta == contaArquivo.Value,
                   $"Linha {linhaNumero}",
                   $"Conta deve ser igual em todas as linhas. Valor esperado: {contaArquivo}.");

            result.AddNotifications(FileKeyValuesContract);

            if (!FileKeyValuesContract.IsValid)
                continue;

            dataEfetivaArquivo ??= dataEfetiva;
            contaArquivo ??= conta;

            var match = Regex.Match(historico, @"^(\d{1,3}) - (.+)$");

            if (!match.Success || !int.TryParse(match.Groups[1].Value, out int codigoHistorico))
            {
                result.AddNotification($"Linha {linhaNumero}", $"Histórico inválido: '{historico}'.");
                continue;
            }

            string descricaoHistorico = match.Groups[2].Value;

            result.Entradas.Add(new Entrada
            {
                Conta = conta,
                CodigoHistorico = codigoHistorico,
                Historico = descricaoHistorico,
                Valor = valor,
                DataEfetiva = dataEfetivaTexto,
                Origem = origem,
                DtApropriacao = dataApropriacao,
                DtProcessamento = dataProcessamento,
                Observacao = observacao
            });

            result.Conta = conta;
        }

        return result;
    }
    private static string ObterCampo(string[] campos, Dictionary<string, int> indices, string header)
    {
        if (!indices.TryGetValue(header, out var indice) || indice >= campos.Length)
            return string.Empty;

        return campos[indice].Trim();
    }

    private static string NormalizarCabecalho(string valor)
    {
        var normalizado = valor.Trim().ToUpperInvariant().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalizado.Length);

        foreach (var c in normalizado)
        {
            if (char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark && c != ' ')
                sb.Append(c);
        }

        return sb.ToString().Replace("DATAETEFIVA", "DATAEFETIVA", StringComparison.Ordinal);
    }
}

