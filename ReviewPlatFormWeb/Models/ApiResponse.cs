using Services.HandlerException;

namespace ReviewPlatFormWeb.Models
{
    public class ApiResponse<T>
    {
        public RequestResultType ResultType { get; set; }
        public List<string>? Messages { get; set; }
        public T? Result { get; set; }
        public string? Exception { get; set; }
    }

}
