using System;

namespace Dictionary.Service.Exceptions;

public class CustomException : Exception
{
    public CustomException(string msg, dynamic data, int code = 400) : base(msg)
    {
        DataErr = data;
        resultCode = code;
    }

    public int resultCode { get; set; }
    public dynamic DataErr { get; set; }
}