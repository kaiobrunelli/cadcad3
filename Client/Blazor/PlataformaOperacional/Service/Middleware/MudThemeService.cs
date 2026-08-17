using MudBlazor;

namespace PlataformaOperacional.Service.Middleware
{
    public class MudThemeService
    {
        private MudTheme _currentTheme;
        private readonly MudTheme _defaultTheme;

        public event Action? OnThemeChanged;

        public MudThemeService()
        {
            // Define o tema padrão inicial
            _defaultTheme = CreateDefaultTheme();
            _currentTheme = _defaultTheme;
        }

        // 2. Criação do tema padrão (com sintaxe correta)
        private static MudTheme CreateDefaultTheme() => new()
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.Blue.Default,
                Secondary = Colors.Teal.Accent4,
                AppbarBackground = Colors.Blue.Darken2,
                TextPrimary = Colors.Gray.Darken4
            }
        };

        // 3. Propriedade para acesso ao tema atual
        public MudTheme CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    NotifyThemeChange();
                }
            }
        }

        /// <summary>
        /// Define um novo tema e notifica os assinantes (layout).
        /// </summary>
        public void SetTheme(MudTheme newTheme)
        {
            CurrentTheme = newTheme;
        }

        /// <summary>
        /// Retorna ao tema que foi definido como padrão na inicialização do serviço.
        /// Chamado ao sair de uma página customizada.
        /// </summary>
        public void SetDefaultTheme()
        {
            SetTheme(_defaultTheme);
        }

      

        private void NotifyThemeChange()
        {
            OnThemeChanged?.Invoke();
        }
    }
}

