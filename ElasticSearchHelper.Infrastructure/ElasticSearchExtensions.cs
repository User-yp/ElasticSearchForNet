using ElasticSearchHelper.Domain.Interfaces;
using ElasticSearchHelper.Domain.Models;
using ElasticSearchHelper.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nest;

namespace ElasticSearchHelper.Infrastructure;

public static class ElasticSearchExtensions
{
    public static void ConfigureElasticSearch(this IServiceCollection services)
    {
        var url=new Uri( Environment.GetEnvironmentVariable("ES:ConnStr"));
        services.AddScoped<IElasticClient>(sp =>
        {
           var settings = new ConnectionSettings(url);
           return new ElasticClient(settings);
        });
        
        services.AddScoped<IElasticSearchService, ElasticSearchService>();
    }
}