

using Microsoft.JSInterop;

namespace PlataformaOperacional.Service.Middleware
{
	public class DownloadService
	{
		private readonly IJSRuntime _jsRuntime;

		public DownloadService(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task<bool> DownloadJS(byte[]? fileBytes, string? fileName)
		{
			try
			{
				await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileBytes);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
