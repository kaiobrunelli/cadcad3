using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaOperacional.Service.Cobranca.Async
{
    /// <summary>
    /// Helpers para executar uma <see cref="Task{T}"/> e materializar o resultado
    /// como um <see cref="AsyncValue{T}"/>, cuidando das transições de estado.
    /// </summary>
    public static class AsyncValueRunner
    {
        /// <summary>
        /// Executa <paramref name="load"/> e notifica a cada transição.
        /// <para>
        /// Fluxo: <c>Loading(previous) → Success | Failure(lastGood)</c>.
        /// Preserva o valor anterior para refresh silencioso e recuperação de erro.
        /// </para>
        /// </summary>
        /// <param name="current">Estado atual (para preservar o último valor bom).</param>
        /// <param name="load">Função que executa a carga.</param>
        /// <param name="onChange">Callback disparado a cada mudança de estado.</param>
        public static async Task RunAsync<T>(
            AsyncValue<T> current,
            Func<CancellationToken, Task<T>> load,
            Action<AsyncValue<T>> onChange,
            CancellationToken ct = default)
        {
            var previous = current.GetValueOrDefault();

            onChange(new AsyncValue<T>.Loading(previous));

            try
            {
                var value = await load(ct).ConfigureAwait(false);
                onChange(new AsyncValue<T>.Success(value));
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                // Cancelamento não é "falha" — volta ao estado anterior coerente.
                onChange(previous is null
                    ? new AsyncValue<T>.Idle()
                    : new AsyncValue<T>.Success(previous));
            }
            catch (Exception ex)
            {
                onChange(new AsyncValue<T>.Failure(ex, previous));
            }
        }
    }
}
