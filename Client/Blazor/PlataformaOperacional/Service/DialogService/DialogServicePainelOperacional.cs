using MudBlazor;
using PlataformaOperacional.Components.BlazorComponentes.Dialog;


namespace PlataformaOperacional.Service
{
	public class DialogServicePlataformaOperacional
    {
        private readonly IDialogService _dialogService;

        public DialogServicePlataformaOperacional(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public Task OpenDialogAsync()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return _dialogService.ShowAsync<Dialog>("Simple Dialog", options);
        }
        public Task OpenDialogAbrirConta(string mensagem, bool sucesso)
        {
            var parameters = new DialogParameters
            {
                {"Mensagem", mensagem  },
                {"Sucesso", sucesso  },
           
            };
            var options = new DialogOptions { CloseOnEscapeKey = true };

            return _dialogService.ShowAsync<Dialog>("Simple Dialog", parameters, options);
        }
    }
}
