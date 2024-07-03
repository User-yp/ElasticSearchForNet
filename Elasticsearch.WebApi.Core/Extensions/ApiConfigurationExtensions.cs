using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Elasticsearch.Domain.IRepository;
using Elasticsearch.Domain.IService;
using Elasticsearch.Infrastructure.Repository;
using Elasticsearch.Infrastructure.Service;
using Elasticsearch.WebApi.Core.Middleware;
using Serilog;

namespace Elasticsearch.WebApi.Core.Extensions;

public static class ApiConfigurationExtensions
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddTransient<IActorsRepository, ActorsRepository>();
        services.AddTransient<IActorsService, ActorsService>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderService, OrderService>();

        services.AddControllers();
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = SerilogExtensions.EnrichFromRequest);

        app.UseMiddleware<RequestSerilLogMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
    }
}
