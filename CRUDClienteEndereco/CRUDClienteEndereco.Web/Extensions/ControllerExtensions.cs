using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CRUDClienteEndereco.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ExecuteAction(this ControllerBase controller, HttpContext context, Func<IActionResult> func)
        {

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                return new JsonResult(new { erro = ex.Message });
            }

        }
    }
}
