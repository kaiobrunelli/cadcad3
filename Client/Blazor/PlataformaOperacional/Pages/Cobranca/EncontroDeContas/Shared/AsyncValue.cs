using System;

namespace PlataformaOperacional.Service.Cobranca.Async
{
    /// <summary>
    /// Representa os 4 estados possíveis de uma operação assíncrona:
    /// <list type="bullet">
    ///   <item><description><see cref="Idle"/> — ainda não foi disparada.</description></item>
    ///   <item><description><see cref="Loading"/> — em execução (primeira carga ou refresh).</description></item>
    ///   <item><description><see cref="Success"/> — concluída com dados.</description></item>
    ///   <item><description><see cref="Failure"/> — falhou com uma exceção.</description></item>
    /// </list>
    /// <para>
    /// É uma <b>discriminated union</b>: em vez de ter 3 booleans soltos
    /// (<c>isLoading</c>, <c>hasError</c>, <c>data != null</c>) espalhados pela UI,
    /// você tem UM valor que, por construção, só pode estar em um estado por vez.
    /// </para>
    /// <para>
    /// Imutável (record) — para atualizar, crie uma nova instância.
    /// </para>
    /// </summary>
    public abstract record AsyncValue<T>
    {
        // Construtor privado impede heranças fora deste arquivo.
        private AsyncValue() { }

        /// <summary>Estado inicial — a carga ainda não foi disparada.</summary>
        public sealed record Idle : AsyncValue<T>;

        /// <summary>
        /// Em carregamento. Guarda opcionalmente o valor anterior, o que permite
        /// "refresh silencioso" na UI (mostra os dados antigos + um spinner discreto)
        /// em vez de piscar um skeleton a cada refetch.
        /// </summary>
        public sealed record Loading(T? Previous = default) : AsyncValue<T>;

        /// <summary>Sucesso — contém o dado carregado.</summary>
        public sealed record Success(T Value) : AsyncValue<T>;

        /// <summary>Falha — contém a exceção. Opcionalmente mantém o último valor bom.</summary>
        public sealed record Failure(Exception Error, T? LastGood = default) : AsyncValue<T>;

        // ---------- Helpers de consulta (evitam pattern matching verboso na UI) ----------

        public bool IsIdle    => this is Idle;
        public bool IsLoading => this is Loading;
        public bool IsSuccess => this is Success;
        public bool IsFailure => this is Failure;

        /// <summary>Retorna o valor se houver (Success ou Loading com Previous), senão default.</summary>
        public T? GetValueOrDefault() => this switch
        {
            Success s              => s.Value,
            Loading l              => l.Previous,
            Failure f              => f.LastGood,
            _                      => default
        };

        /// <summary>
        /// Dobra (fold) os 4 estados em um único valor. Útil pra renderização:
        /// <code>
        /// var texto = estado.Match(
        ///     onIdle:    ()      => "Aguardando…",
        ///     onLoading: prev    => "Carregando…",
        ///     onSuccess: v       => $"OK: {v}",
        ///     onFailure: (e, _)  => $"Erro: {e.Message}");
        /// </code>
        /// </summary>
        public TResult Match<TResult>(
            Func<TResult> onIdle,
            Func<T?, TResult> onLoading,
            Func<T, TResult> onSuccess,
            Func<Exception, T?, TResult> onFailure) => this switch
            {
                Idle              => onIdle(),
                Loading l         => onLoading(l.Previous),
                Success s         => onSuccess(s.Value),
                Failure f         => onFailure(f.Error, f.LastGood),
                _                 => throw new InvalidOperationException("Estado impossível")
            };
    }
}
