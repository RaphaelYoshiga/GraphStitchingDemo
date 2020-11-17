using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using RYoshiga.HotChocolateDemo.DataLoaders;
using RYoshiga.HotChocolateDemo.Models;

namespace RYoshiga.HotChocolateDemo.QueryTypes
{
    public class ItemType : ObjectType<Item>
    {
        protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor
                .Field(t => t.Product)
                .ResolveWith<ProductResolver>(t => t.GetProductAsync(default!, default!, default!, default));
        }
        private class ProductResolver
        {
            public async Task<Product> GetProductAsync(
                Item item,
                ProductsByIdDataLoader productById,
                CancellationToken cancellationToken,
                IResolverContext context)
            {
                return await productById.LoadAsync(item.ProductId, cancellationToken);
            }
        }
    }
}