namespace AppwriteClient.Models
{
    public class AppWriteHttpRequestException : Exception
    {
        public AppWriteHttpRequestException(string message, int statusCode, string type)
        {
            Message = message;
            StatusCode = statusCode;
            Type = type;
        }

        public string Message { get; }
        public int StatusCode { get; }
        public string Type { get; set; }
    }
}
