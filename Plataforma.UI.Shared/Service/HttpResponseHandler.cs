//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;

//namespace Plataforma.UI.Shared.Service
//{
//    public class HttpResponseHandler
//    {

//        public static async Task<T?> ResponseHandler<T>(HttpResponseMessage response, ISnackbar snackBar) where T : notnull
//        {
//            if (response.IsSuccessStatusCode)
//            {
//                var conteudo = await response.Content.ReadFromJsonAsync<T>();
//                if (conteudo is null) throw new Exception();
//                return conteudo;
//            }
//            else
//            {
//                var responseProblemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
//                if (responseProblemDetail == null) throw new Exception("ProblemDetail nulo.");
//                snackBar.Add(responseProblemDetail.Detail, Severity.Error);

//            }
//            return default;
//        }
//        public static async Task ResponseHandler(HttpResponseMessage response, ISnackbar snackBar)
//        {
//            if (response.IsSuccessStatusCode)
//            {
//                var mensagemResposta = await response.Content.ReadAsStringAsync();
//                snackBar.Add(mensagemResposta, Severity.Success);
//            }
//            else
//            {
//                var responseProblemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
//                if (responseProblemDetail == null) throw new Exception("ProblemDetail nulo.");
//                snackBar.Add(responseProblemDetail.Detail, Severity.Error);
//            }
//        }
//        public static async Task<string?> ResponseHandlerHeader(HttpResponseMessage response, ISnackbar snackBar)
//        {
//            if (response.IsSuccessStatusCode)
//            {
//                if (response.Headers.TryGetValues("Location", out var responseHeader))
//                {
//                    var chave = responseHeader.FirstOrDefault();
//                    if (!string.IsNullOrEmpty(chave))
//                    {
//                        return chave;
//                    }
//                }
//                Console.WriteLine("Chave signalR não localizada!");
//                return null;
//            }
//            else
//            {
//                var responseProblemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
//                if (responseProblemDetail == null) throw new Exception("ProblemDetail nulo.");
//                snackBar.Add(responseProblemDetail.Detail, Severity.Error);
//                return null;
//            }
//        }
//    }
//}
