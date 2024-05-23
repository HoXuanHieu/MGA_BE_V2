namespace Web_API.ResponseModel
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }
        public int Status { get; set; }
    }
}
