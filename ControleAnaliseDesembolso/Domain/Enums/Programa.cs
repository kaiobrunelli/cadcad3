using System.ComponentModel.DataAnnotations;

namespace ControleAnaliseDesembolso.Domain.Enums
{
    public enum Programa
    {
        [Display(Name = "Pró-Tansporte")]
        Pro_Transporte,
        [Display(Name = "Pró-Moradia")]
        Pro_Moradia,
        [Display(Name = "Saneamento Para Todos")]
        Saneamento,
        [Display(Name = "FGTS-Saúde")]
        Saude
    }
}
