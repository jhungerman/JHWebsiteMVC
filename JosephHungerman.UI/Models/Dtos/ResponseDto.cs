namespace JosephHungerman.UI.Models.Dtos
{
    public enum ErrorResponseType
    {
        Exception,
        NotFound,
        Unauthorized,
        BadRequest,
        Database
    }

    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public object? Result { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public ErrorResponseType? ErrorResponseType { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
