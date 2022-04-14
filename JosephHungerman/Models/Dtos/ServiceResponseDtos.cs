﻿namespace JosephHungerman.Models.Dtos
{
    public class ServiceResponseDtos<TResultType> where TResultType : class, new()
    {
        public class ServiceSuccessResponse : ResponseDto
        {
            public ServiceSuccessResponse(TResultType result)
            {
                IsSuccess = true;
                DisplayMessage = string.Empty;
                ErrorResponseType = null;
                ErrorMessages = null;
                Result = result;
            }
        }
        public class ServiceExceptionResponse : ResponseDto
        {
            public ServiceExceptionResponse(Exception e)
            {
                IsSuccess = false;
                DisplayMessage = string.Empty;
                ErrorResponseType = Dtos.ErrorResponseType.Exception;
                ErrorMessages = new List<string> { e.ToString() };
                Result = null;
            }
        }
        public class ServiceNotFoundExceptionResponse : ResponseDto
        {
            public ServiceNotFoundExceptionResponse(Exception? e = null)
            {
                IsSuccess = false;
                DisplayMessage = string.Empty;
                ErrorResponseType = Dtos.ErrorResponseType.NotFound;
                ErrorMessages = new List<string> { "NOT FOUND" };
                Result = null;
            }
        }
        public class ServiceDbExceptionResponse : ResponseDto
        {
            public ServiceDbExceptionResponse()
            {
                IsSuccess = false;
                DisplayMessage = string.Empty;
                ErrorResponseType = Dtos.ErrorResponseType.Database;
                ErrorMessages = new List<string> { "Database exception", "Please try again later" };
                Result = null;
            }
        }
        public class ServiceBadRequestExceptionResponse : ResponseDto
        {
            public ServiceBadRequestExceptionResponse(Exception? e = null)
            {
                IsSuccess = false;
                DisplayMessage = string.Empty;
                ErrorResponseType = Dtos.ErrorResponseType.BadRequest;
                ErrorMessages = e == null ? new List<string> { "Bad request" } : new List<string> { e.ToString() };
                Result = null;
            }
        }
    }
}
