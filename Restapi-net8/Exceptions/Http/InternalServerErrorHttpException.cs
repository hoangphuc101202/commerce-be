﻿using System.Net;
using System.Runtime.Serialization;

namespace Restapi_net8.Exceptions.Http
{
    public class InternalServerErrorHttpException : HttpException
    {
        #region CONSTANTS

        private const HttpStatusCode ExceptionStatusCode = HttpStatusCode.InternalServerError;

        #endregion CONSTANTS

        #region CONSTRUCTORS

        public InternalServerErrorHttpException(string message) : base(ExceptionStatusCode, message)
        {
        }

        public InternalServerErrorHttpException(string message, Exception inner) : base(ExceptionStatusCode, message, inner)
        {
        }

        public InternalServerErrorHttpException() : base(ExceptionStatusCode)
        {
        }

        protected InternalServerErrorHttpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion CONSTRUCTORS
    }
}
