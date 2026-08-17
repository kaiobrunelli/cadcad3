//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Microsoft.AspNetCore.SignalR.Client;
//using PlataformaOperacional.Model.Plataforma;
//using System.Collections.Concurrent;

//namespace PlataformaOperacional.Service.Middleware
//{

//    public class SignalRService : IAsyncDisposable
//    {
//        private readonly HttpClient _httpClient;
//        private readonly HttpClient _httpLocal;
//        private readonly BlazorMockService _mockBlazor;
//        private HubConnection _hubConnection;
//        private Task _startTask;

//        public string _baseAdress => _mockBlazor.MockarDados ? _httpLocal.BaseAddress.ToString() : _httpClient.BaseAddress.ToString();

//        public SignalRService(IHttpClientFactory httpClientFactory, BlazorMockService blazorMockService)
//        {
//            //_httpClient = httpClientFactory.CreateClient(ClientName);
//            _httpClient = httpClientFactory.CreateClient("Api");

//            _httpLocal = httpClientFactory.CreateClient("ApiLocal");
//            _mockBlazor = blazorMockService;
//            CriarHubUrl(_baseAdress);
//        }

//        // Controla as conexões ativas no SignalR (o ".On") para não duplicar listeners na mesma chave
//        private readonly ConcurrentDictionary<string, IDisposable> _registeredKeys = new();

//        // Armazena o último estado recebido (Cache) para entregar a novos observadores imediatamente (ex: troca de aba)
//        private readonly ConcurrentDictionary<string, ObservadorAutomacao> _progressoAtualPorHub = new();

//        // LISTA de Callbacks: Permite que vários componentes ou abas escutem a mesma chave
//        private readonly ConcurrentDictionary<string, List<Func<ObservadorAutomacao, Task>>> _observersPorChave = new();

//        // URL do Hub (Ajuste conforme sua necessidade real)
//        //private readonly string _hubUrlProd = "https://www.ativo.fgts.caixa/PlataformaOperacional/chatHub";
//        private string _hubUrlProd;
//        public string HubUrlProd => _hubUrlProd;

//        // Controle de conclusão
//        private ConcurrentDictionary<string, bool> _flagCompletouPorHub = new();

//        // Evento global opcional
//        public event Action<bool> OnProgressUpdateCompleted;
//        public event Action<string> OnProgressUpdateCompletedByKey;

//        public void CriarHubUrl(string url)
//        {
//            _hubUrlProd = $"{url}chatHub";
//        }

//        public Task IniciarHubConnection()
//        {
//            if (_hubConnection != null && _hubConnection.State == HubConnectionState.Connected)
//            {
//                return Task.CompletedTask;
//            }

//            var existingTask = _startTask;
//            if (existingTask != null && !existingTask.IsFaulted && !existingTask.IsCanceled)
//            {
//                return existingTask;
//            }

//            var newTask = StartHubInternalAsync();
//            var winner = Interlocked.CompareExchange(ref _startTask, newTask, existingTask);

//            return winner == existingTask ? newTask : winner;
//        }

//        private async Task StartHubInternalAsync()
//        {
//            if (_hubConnection == null)
//            {
//                var built = new HubConnectionBuilder()
//                  .WithUrl(_hubUrlProd)
//                  .WithAutomaticReconnect()
//                  .Build();

//                Interlocked.CompareExchange(ref _hubConnection, built, null);
//            }

//            if (_hubConnection.State == HubConnectionState.Disconnected)
//            {
//                try
//                {
//                    await _hubConnection.StartAsync();
//                }
//                catch (Exception ex)
//                {
//                    throw;
//                }
//            }
//        }

//        /// <summary>
//        /// Adiciona um componente interessado na lista de notificações dessa chave.
//        /// </summary>
//        public void RegistrarObserver(string chave, Func<ObservadorAutomacao, Task> callback)
//        {
//            _observersPorChave.AddOrUpdate(chave,
//                // Se a chave não existe, cria uma nova lista com o callback
//                new List<Func<ObservadorAutomacao, Task>> { callback },
//                // Se a chave já existe, adiciona o callback na lista existente
//                (key, list) =>
//                {
//                    lock (list) // Lock para garantir que não haja conflito de thread
//                    {
//                        if (!list.Contains(callback))
//                        {
//                            list.Add(callback);
//                        }
//                    }
//                    return list;
//                });

//            // Se já temos dados em cache para essa chave, entregamos imediatamente para a UI não ficar vazia
//            if (_progressoAtualPorHub.TryGetValue(chave, out var ultimoEstado))
//            {
//                // Executa sem await (fire-and-forget) para não travar a thread atual
//                _ = callback.Invoke(ultimoEstado);
//            }
//        }

//        /// <summary>
//        /// [IMPORTANTE] Remove um componente específico da lista de notificações.
//        /// Chamado no Dispose do componente Blazor.
//        /// </summary>
//        public void RemoverObserver(string chave, Func<ObservadorAutomacao, Task> callback)
//        {
//            if (_observersPorChave.TryGetValue(chave, out var list))
//            {
//                lock (list) // Lock é essencial aqui para não corromper a lista enquanto ela está sendo lida no loop
//                {
//                    list.Remove(callback);
//                }
//            }
//        }

//        /// <summary>
//        /// Inicia a escuta real no SignalR (.On).
//        /// Gerencia a distribuição das mensagens para todos os observers da lista.
//        /// </summary>
//        public async Task IniciarEscutaDaOperacao(string hubConnectId)
//        {
//            if (_registeredKeys.ContainsKey(hubConnectId))
//            {
//                return;
//            }

//            try
//            {
//                await IniciarHubConnection();

//                _registeredKeys.GetOrAdd(hubConnectId, key =>
//                {
//                    return _hubConnection.On<int, ObservadorAutomacao>(key, async (progresso, observer) =>
//                    {
//                        // 1. Atualiza o cache local
//                        _progressoAtualPorHub[key] = observer;

//                        // 2. Verifica se existem componentes ouvindo essa chave
//                        if (_observersPorChave.TryGetValue(key, out var callbacksList))
//                        {
//                            Func<ObservadorAutomacao, Task>[] callbacksSnapshot;

//                            // 3. Cria uma cópia segura da lista para iterar
//                            // Isso evita erro de "Coleção modificada" se um componente der Dispose enquanto o loop roda
//                            lock (callbacksList)
//                            {
//                                callbacksSnapshot = callbacksList.ToArray();
//                            }

//                            // 4. Dispara a atualização para todos os componentes (Aba 1, Aba 2, Componente X...)
//                            if (callbacksSnapshot.Length > 0)
//                            {
//                                await Task.WhenAll(callbacksSnapshot.Select(cb => cb(observer)));
//                            }
//                        }

//                        // 5. Gerencia conclusão
//                        if (observer.PercentualProcessado == 100 && !_flagCompletouPorHub.GetValueOrDefault(key))
//                        {
//                            _flagCompletouPorHub[key] = true;
//                            OnProgressUpdateCompleted?.Invoke(false);
//                            OnProgressUpdateCompletedByKey?.Invoke(key);
//                        }
//                    });
//                });
//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//        }

//        public void InterromperEscutaDaOperacao(string hubConnectId)
//        {
//            // Remove a escuta do SignalR (Para de receber dados da rede para essa chave)
//            if (_registeredKeys.TryRemove(hubConnectId, out var subscription))
//            {
//                subscription.Dispose();

//                // Limpa caches
//                _progressoAtualPorHub.TryRemove(hubConnectId, out _);
//                _flagCompletouPorHub.TryRemove(hubConnectId, out _);

//                // Opcional: Limpa a lista de observers se a conexão for derrubada forçadamente
//                // _observersPorChave.TryRemove(hubConnectId, out _); 

//            }
//        }

//        public async Task ReiniciarEscutaDaOperacao(string hubConnectId)
//        {
//            InterromperEscutaDaOperacao(hubConnectId);
//            await IniciarEscutaDaOperacao(hubConnectId);
//        }

//        public Task<ObservadorAutomacao?> ObterEstadoAtual(string hubConnectId)
//        {
//            if (_progressoAtualPorHub.TryGetValue(hubConnectId, out var observer))
//            {
//                return Task.FromResult<ObservadorAutomacao?>(observer);
//            }
//            return Task.FromResult<ObservadorAutomacao?>(null);
//        }

//        public async ValueTask DisposeAsync()
//        {
//            if (_hubConnection is not null)
//            {
//                await _hubConnection.DisposeAsync();
//            }

//            foreach (var subscription in _registeredKeys.Values)
//            {
//                try { subscription.Dispose(); }
//                catch { }
//            }
//            _registeredKeys.Clear();
//            _observersPorChave.Clear();
//            _progressoAtualPorHub.Clear();
//            _flagCompletouPorHub.Clear();
//        }
//    }
//}









