using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GameWebApi
{
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = context.Exception.Message;

                var exceptionType = context.Exception.GetType();

            //Checking for my custom exception type
            if (context.Exception.GetType() == typeof(LevelException))
            {
                message = context.Exception.Message;
            }


            //You can enable logging error

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new ApiResponse { Message = message, Data = null });

            //throw new NotImplementedException();
        }
    }
}
