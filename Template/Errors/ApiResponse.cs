namespace Template.Errors
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ApiResponse(int code, string massage)
        {
            Status = code;
            Message = massage;

        }
        public ApiResponse(int code)
        {
            Status = code;
            Message = GetDefaultMessage(code);
        }

        private string GetDefaultMessage(int code)
        {
            return code switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "An unexpected error occurred"
            };
        }
    }
}
