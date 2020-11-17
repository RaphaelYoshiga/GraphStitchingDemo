using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Types;
using RYoshiga.HotChocolateDemo.Models;

namespace RYoshiga.HotChocolateDemo.QueryTypes
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor
                .Field(t => t.Orders)
                .ResolveWith<OrderResolver>(t => t.GetSessionsAsync(default!, default));
        }
    }
    public class OrderResolver
    {
        public async Task<IEnumerable<Order>> GetSessionsAsync(
            Customer customer,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<Order>()
            {
                new Order
                {
                    Total = 864.99m,
                    Items = new List<Item>
                    {
                        new Item
                        {
                            UnitCost = 500.99m,
                            ProductId = Demo.ProductId,
                            Quantity = 1
                        },
                        new Item{
                            UnitCost = 99,
                            ProductId = Demo.ProductId2,
                            Quantity = 1
                        },
                        new Item{
                            UnitCost = 55,
                            ProductId = Demo.ProductId3,
                            Quantity = 3
                        },
                        new Item{
                            UnitCost = 25,
                            ProductId = Demo.ProductId4,
                            Quantity = 4
                        }
                    }
                }
            }.AsEnumerable());
        }
    }
}

