using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RYoshiga.ProductService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddQueryType<ProductQuery>()
                .AddServices(sp)
                .Create());

            services.AddDataLoader<ProductsByIdDataLoader>();

            services.AddErrorFilter<GraphQLErrorFilter>();
            //services.AddDataLoader<ProductsByIdDataLoader>();
            //services.AddTransient<IOrderRepository, OrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UsePlayground(new PlaygroundOptions()
            {
                Path = "/playground",
                QueryPath = "/graph"
            });
            app.UseGraphQL("/graph");
        }
    }

    public class ProductQuery
    {
        public async Task<Product> Product([DataLoader] ProductsByIdDataLoader dataLoader, Guid id, CancellationToken token)
        {
            return await dataLoader.LoadAsync(id, token);
        }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }


    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            var message = error.Exception?.Message ?? error.Message;
            return error.WithMessage(message);
        }
    }

}
