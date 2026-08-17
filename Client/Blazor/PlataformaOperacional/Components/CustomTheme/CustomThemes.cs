using MudBlazor;

namespace PlataformaOperacional.Componentes.CustomTheme
{
    public  class CustomThemes
    {

        public static MudTheme LightThemeCustom = new MudTheme
        {
  
            PaletteLight = new PaletteLight
            {
                Primary = "#d87b00", // Cor Primary DSC (laranja)
                Secondary = "#005CA9",   // Cor Secondary DSC (azul)
                //Tertiary = "#3080E3", // Cor Auxiliar DSC 
                Tertiary = "#f39200", // Cor Auxiliar DSC 
                PrimaryLighten = "#000000",
                
                Background = "#ffffff", // Cor de fundo
                Surface = "#ffffff", // Cor de superfície (para itens como cards, tabelas)
                AppbarBackground = "#EF765E", // Cor de fundo da barra de navegação
                AppbarText = "#ffffff", // Cor do texto da barra de navegação
                DrawerBackground = "#00437A", // Cor do fundo da gaveta lateral
                DrawerText = "#ffffff", // Cor do texto da gaveta lateral
                TextPrimary = "#404B52", // Cor do texto primário (CINZA)
                TextSecondary = "#005CA9", // Cor do texto secundário (AZUL CAIXA)
                Error = "#b22c2c", // Cor de erro (vermelho)
                //Success = "#4caf50", // Cor de sucesso (verde)
                Success = "#0BAE10", // Cor de sucesso (verde)
                Info = "#2196f3", // Cor de informação (azul)
                //Warning = "#ff9800", // Cor de aviso (laranja) CORRETO <<<<<<<<<<
                //Warning = "#EFF5F6", // Cor de aviso (laranja)
                Warning = "#F39200", // Cor de aviso (laranja)
                //Divider = "#e0e0e0", // Cor do divisor (linha separadora) CORRETO <<<<<<<<<<
                Divider = "#005CA9", // Cor do divisor (linha separadora)
                DrawerIcon = "#ffffff", 


            },
            Typography = new Typography
            {
           
                Default = new DefaultTypography()
                {
                    FontFamily = ["CAIXA STD", "Arial"]
                       //FontFamily = ["CAIXA STD", "Arial", "sans-serif"]
                },                
                H1 = new H1Typography() { FontSize = "2.50rem", FontWeight ="600", LineHeight = "1.25" },
                H2 = new H2Typography() { FontSize = "2.25rem", FontWeight ="600", LineHeight = "1.25" },
                H3 = new H3Typography() { FontSize = "2rem",    FontWeight ="600", LineHeight = "1.25" },
                H4 = new H4Typography() { FontSize = "1.75rem", FontWeight ="600", LineHeight = "1.25" },
                H5 = new H5Typography() { FontSize = "1.50rem", FontWeight ="600", LineHeight = "1.25" },
                H6 = new H6Typography() { FontSize = "1.25rem", FontWeight ="600", LineHeight = "1.25" },
                
                Body1 = new Body1Typography() { FontSize = "1rem",     FontWeight = "400", LineHeight = "1.5" },
                Body2 = new Body2Typography() { FontSize = "0.875rem", FontWeight = "400", LineHeight = "1.43"},
               
            }          
           
        };
    }
}
