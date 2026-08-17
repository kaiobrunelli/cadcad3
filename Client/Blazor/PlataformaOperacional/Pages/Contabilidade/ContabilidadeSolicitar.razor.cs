
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace PlataformaOperacional.Pages.Contabilidade
{
  
    public partial class ContabilidadeSolicitar
    {

        public IBrowserFile InputArquivoCarregado; 

        private string DataInicial { get; set; } = "";
        private string DataFinal { get; set; } = "";
        private string NumeroContrato { get; set; } = "";
        private List<string> ListaContratos { get; set; } = new();
        private string MensagemDeErro { get; set; } = "";
         
   

        public void OnFileSelect (IBrowserFile file)
        {
            InputArquivoCarregado=file;
           
        }
       
        void Closed(MudChip<string> chip, string contrato)
        {
            ListaContratos.Remove(contrato);
        }

        public void AddContract()
        {


            if (!string.IsNullOrWhiteSpace(NumeroContrato))
            {
                ListaContratos.Add(NumeroContrato);
                NumeroContrato = string.Empty;
            }
        }

        private void RemoveContract(string contrato)
        {
            ListaContratos.Remove(contrato);
        }
        private void CarregarCSV()
        {

        }

        private void GerarLote()
        {
            if (InputArquivoCarregado != null || ListaContratos.Count> 0)
            {
                DialogServicePlataformaOperacional.OpenDialogAsync();
            }
            else
            {
                MensagemDeErro = "Necessário adicionar  contrato ou carregar arquivo para gerar lote.";
            }

        }

    }

}

