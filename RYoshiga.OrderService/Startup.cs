using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RYoshiga.OrderService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(sp => SchemaBuilder.New()
                .AddQueryType<OrderQuery>()
                .AddServices(sp)
                .Create());

            services.AddTransient<IOrderRepository, OrderRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UsePlayground(new PlaygroundOptions()
            {
                Path = "/playground",
                QueryPath = "/graph"
            });
            app.UseGraphQL("/graph");
        }
    }

    public class OrderQuery
    {
        public ICollection<Order> Orders([Service]IOrderRepository repository, int customerId) => repository.GetOrders(customerId);
    }

    public interface IOrderRepository
    {
        ICollection<Order> GetOrders(in int customerId);
    }

    public class OrderRepository : IOrderRepository
    {
        public ICollection<Order> GetOrders(in int customerId)
        {
            return new List<Order>
            {
                new Order
                {
                    Total = 111,
                    Items = new List<Item>
                    {
                        new Item
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 1,
                            UnitCost = 1
                        },
                        new Item
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 13,
                            UnitCost = 13
                        },
                        new Item
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 14,
                            UnitCost = 14
                        }
                        , new Item
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 12,
                            UnitCost = 12
                        }
                    }
                }
            };
        }
    }
}
