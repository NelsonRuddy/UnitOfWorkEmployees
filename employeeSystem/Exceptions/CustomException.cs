using System;
using System.Net;

namespace employeeSystem.Api.Exceptions
{
    public class CustomException : Exception
    {

        public HttpStatusCode StatusCode { get; }

        public CustomException() { }

        public CustomException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
