namespace Presentation.Models;

public class AuthServiceResult
{
    public bool Succeeded { get; set; }
    
    public string? Error  { get; set; }
}

public class AuthServiceResult<T> : AuthServiceResult
{
    public T? Result { get; set; }
}