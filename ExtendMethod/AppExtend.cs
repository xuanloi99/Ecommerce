using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ecommerce.ExtendMethod
{
    public static class AppExtend 
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(appErr =>
            {
                appErr.Run(async context =>
                {
                    var response = context.Response;
                    var code = response.StatusCode;

                    var content = @$"<html>
                        <head>
                            <meta charset='UTF-8' />
                            <title>Error {code}</title>
    

                        </head>
                        <body>
                            <p style='color:red; font-size:20px'> Error: {code} -{(HttpStatusCode)code} </p>
                        </body>

                        </html>";
                    await response.WriteAsync(content);
                });
            });
        }
    }
}
