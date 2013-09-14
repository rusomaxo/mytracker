using System;
using System.Collections.Generic;

namespace mytracker.api.Exceptions
{
    /// <summary>
    /// dublicate user exception
    /// </summary>
    public class ApiException : ApplicationException
    {
        public ApiException(ApiExceptionType exceptionType)
        {
            ExceptionType = exceptionType;
            Values = new Dictionary<string, object>();
        }

        /// <summary>
        /// type of api expression
        /// </summary>
        public ApiExceptionType ExceptionType { get; protected set; }

        /// <summary>
        /// exception values
        /// </summary>
        public Dictionary<string, object> Values { get; private set; }
    }
}
