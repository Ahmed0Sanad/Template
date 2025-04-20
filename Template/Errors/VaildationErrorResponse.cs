namespace Template.Errors
{
    public class VaildationErrorResponse : ApiResponse
    {
        public VaildationErrorResponse(List<string> errors) : base(400)
        {
            Errors = errors;
        }

        public List<string> Errors { get; }
    }
}
