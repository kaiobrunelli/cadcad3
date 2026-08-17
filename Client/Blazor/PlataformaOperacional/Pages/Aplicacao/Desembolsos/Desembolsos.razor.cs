using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PlataformaOperacional.Pages.Aplicacao.Desembolsos;
public partial class Desembolsos : ComponentBase
{
    public static readonly MudTheme DSCtheme = new()
    {
        PaletteLight = new PaletteLight
        {
            //Azul
            Primary = "#005CA9", //Primary 90
            PrimaryDarken = "#00437A", //Primary 110
            PrimaryLighten = "#2D8AD8", //Primary 70
                                        //Laranja
            Secondary = "#d87b00", //Secondary 90
            SecondaryDarken = "#a65e00", //Secondary 110
            SecondaryLighten = "#f39200", //Secondary 70
                                          //Turquesa
            Tertiary = "#54bbab", //Tertiary 70
            TertiaryDarken = "#359485", //Tertiary 90
            TertiaryLighten = "#81d6c8", //Tertiary 50
                                         //Cinza
            GrayDefault = "#d0e0e3", //50
            GrayDark = "#9eb2b8", //70
            GrayDarker = "#64747a", //90
            GrayLight = "#eBf1f2", //30
            GrayLighter = "#f7fAfa", //10
                                     //Branco
            White = "#FFFFFF",
            //Positive
            Success = "#127527", //90
            SuccessDarken = "#0d581c", //110
            SuccessLighten = "#179231", //70
                                        //Attention
            Warning = "#ca9804", //90
            WarningDarken = "#977203", //110
            WarningLighten = "#fcbe05", //70
                                        //Negative
            Error = "#b22c2c", //90
            ErrorDarken = "#8c2323", //110
            ErrorLighten = "#d93636", //70
                                      //Informative
            Info = "#038299", //90
            InfoDarken = "#026173", //110
            InfoLighten = "#04a2bf", //70
        },
    };
}
