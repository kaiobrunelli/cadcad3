namespace PlataformaOperacional.Service.Middleware
{
	public class BlazorMockService
	{
		public BlazorMockService(bool mockarDados)
		{
			MockarDados = mockarDados;
		}

		public bool MockarDados { get; set; }	
	}
}
