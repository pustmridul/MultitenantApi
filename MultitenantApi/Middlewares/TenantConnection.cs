using Microsoft.EntityFrameworkCore;
using MultitenantApi.Data;

namespace MultitenantApi.Middlewares
{
    public class TenantConnection
    {

        private readonly RequestDelegate _next;

        public TenantConnection(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, AppDbContext appDbCtx)
        {
            if (httpContext.Request.Headers.Any(h => h.Key.ToLower() == "admin"))
                await _next.Invoke(httpContext);
            else
            {
                string customerName = httpContext.Request.Headers.FirstOrDefault(h => h.Key == "ClientName").Value;
                if (string.IsNullOrEmpty(customerName))
                    throw new Exception("Client not found!");
                await using var mainCtx = new MainContext();
                var customer = mainCtx.Clients.FirstOrDefault(c => customerName.ToLower() == c.Name.ToLower());
                //you can verify customer
                appDbCtx.Database.SetConnectionString(customer?.ConnectionString);
                await _next.Invoke(httpContext);
            }
        }
    }
}
