using System;
using System.Net.Http.Headers;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Stitching;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RYoshiga.GraphStitcher
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var graphProviders = new[]
            {
                new GraphProvider()
                {
                    SchemaName = "profile",
                    Url = "http://localhost:5000/graph/"
                },
                new GraphProvider()
                {
                    SchemaName = "orders",
                    Url = "https://localhost:5003/graph/"
                }
            };

            services.AddHttpContextAccessor();

            services.AddStitchedSchema(builder =>
            {
                foreach (var graph in graphProviders)
                {
                    ConfigureHttpClient(services, graph);

                    builder.AddSchemaFromHttp(graph.SchemaName);
                }

           //     builder.AddExtensionsFromFile("./graphql/Extensions.graphql");
            });

            services.AddDataLoaderRegistry();
            services.AddGraphQLSubscriptions();
            services.AddErrorFilter<GraphQLErrorFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePlayground(new PlaygroundOptions()
            {
                Path = "/playground",
                QueryPath = "/graph"
            });
            app.UseGraphQL("/graph");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private static void ConfigureHttpClient(IServiceCollection services, GraphProvider graph)
        {
            services.AddHttpClient(graph.SchemaName, (sp, client) =>
            {
                client.BaseAddress = new Uri(graph.Url);

                var context = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
                if (context == null || !context.Request.Headers.ContainsKey("Authorization"))
                    return;

                var authorization = context.Request.Headers["Authorization"].ToString();
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authorization);
            });
        }
    }

    internal class GraphProvider
    {
        public string Url { get; set; }
        public string SchemaName { get; set; }

    }

    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Exception.Message);
        }
    }

    public class Query
    {
    }
}
