namespace Restapi_net8.Middlewares
{
    public class ApiResponse
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }
        public ApiResponse(int statusCode, string message, object data, string error ) {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Error = error;
        }
    }
}
