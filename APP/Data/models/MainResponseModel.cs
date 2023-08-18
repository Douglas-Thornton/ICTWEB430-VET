namespace APP.Data.Models;

public class MainResponseModel
{
    public bool IsSuccess { get; set; }
    public string ErrorMEssage { get; set; }
    public object Content { get; set; }
}
