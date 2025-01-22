using eCommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string message = "sorry, internal server error occurred, Kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string tittle = "Error";

            try
            {
                await next(context);
                //Check if Response is Too Many Request
                if(context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    tittle = "Warning";
                    message = "Too many request";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(context, tittle, message, statusCode);
                }

                //Check if Reponse is Unauthorized 
                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    tittle = "Alert";
                    message = "You are not authorized to access";
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                    await ModifyHeader(context, tittle, message, statusCode);
                }

                //Chech if Response is forbidden
                if(context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    tittle = "Out of Access";
                    message = "You are not allowed to access";
                    statusCode = (int)StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, tittle, message,statusCode);
                }

            }
            catch (Exception ex)
            {
                //Log Original Exception
                LogException.LogExceptions(ex);

                //Check if exception is timeout
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    tittle = "Out of time";
                    message = "Request timeout, try again";
                    statusCode = (int)StatusCodes.Status408RequestTimeout;
                }


                await ModifyHeader(context, tittle, message, statusCode);
            }
        }

        private static async Task ModifyHeader(HttpContext context, string tittle, string message, int statusCode)
        {
            // Display scary-free message to client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Detail = message,
                Status = statusCode,
                Title = tittle
            }), CancellationToken.None);
            return;
        }
    }
}
