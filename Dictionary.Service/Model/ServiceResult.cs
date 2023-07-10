namespace Dictionary.Service.Model;

public class ServiceResult
{
    public ServiceResult(int status, string message, string code, dynamic data, int errorCode)
    {
        Status = status;
        Message = message;
        Code = code;
        Data = data;
        ErrorCode = errorCode;
    }

    public int Status { get; set; }


    public string Message { get; set; }

    public string Code { get; set; }

    public dynamic Data { get; set; }

    public int ErrorCode { get; set; }
}