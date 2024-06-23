namespace Web_API.ResponseModel
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }
        public int Status { get; set; }

        public ApiResponse(string message, T content, int status)
        {
            this.Content = content;
            this.Status = status;
            if (message == null || message == "")
            {
                this.Message = GetMessage(status);
            }
            else
            {
                this.Message = message;
            }
        }
        private String GetMessage(int status)
        {
            return status switch
            {
                //SUCCESS
                200 => "",
                201 => "",
                //CLIENT ERROR
                400 => "",
                401 => "",
                //INTERNAL ERROR
                500 => "",
            };
        }
    }
}
