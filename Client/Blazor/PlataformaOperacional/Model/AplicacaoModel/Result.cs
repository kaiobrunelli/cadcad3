using MudBlazor;

namespace PlataformaOperacional.Model.AplicacaoModel
{
	public class Result<T> 
	{
		public bool IsSuccess { get; }
		public T Value { get; }
		public List<string> Errors { get; }

		public Result(T value)
		{
			IsSuccess = true;
			Value = value;
			Errors = new List<string>();
		}

		public Result(List<string> errors)
		{
			IsSuccess = false;
			Errors = errors;
			Value = default;
		}

		public static Result<T> Success(T value) => new Result<T>(value);
		public static Result<T> Failure(List<string> errors) => new Result<T>(errors);
	}
}